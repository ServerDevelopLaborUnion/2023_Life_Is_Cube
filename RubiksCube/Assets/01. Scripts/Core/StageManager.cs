using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] List<DirectionFlagList> ignoreAxes;

    [field: SerializeField] public int RotateCount { get; set; }
    [SerializeField] float startDelayTime = 1.8f;

    private Cube cube;
    private PreparedCube preparedCube;
    private EnemyFactory enemyFactory;

    private Transform preparedCubes = null;

    private List<AIBrain> enemyList = new List<AIBrain>();

    private bool stageChanged = false;
    public bool StageChanged { get => stageChanged; set => stageChanged = value; }

    [SerializeField] int midBossTrigger = 3;
    private int stageProgress = 0;

    #region 테스트
    private ParticleSystem midBossParticle;
    #endregion

    private void Awake()
    {
        cube = GameObject.Find("Cube").GetComponent<Cube>();
        preparedCube = GameObject.Find("PreparedCubes").GetComponent<PreparedCube>();
        enemyFactory = cube.GetComponent<EnemyFactory>();

        midBossParticle = GameObject.Find("MidBossParticle").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartGame();
    }

    public Coroutine RotateDirecting() => StartCoroutine(RotateDirectingCoroutine());

    private IEnumerator RotateCoroutine(int cnt)
    {
        cube.SortCellIndexes();
        int currentCellIdx = cube.GetCurrentCell().CellIndex;

        for (int i = 0; i < cnt;)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);

            if (ignoreAxes.Count > currentCellIdx)
            {
                if (ignoreAxes[currentCellIdx].flags.Contains((DirectionFlags)axis) == false)
                {
                    i++;
                    yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
                }
            }
        }

        cube.SortCellIndexes();
        for (int i = 0; i < cube.ActivatedCells.Count; i++)
        {
            Vector3[] pushOrders = cube.ActivatedCells[i].CheckNeighborCell();

            foreach (Vector3 dir in pushOrders)
            {
                cube.CubeConfiner.SetActiveCollider(i, dir.normalized, false);
                cube.ActivatedCells[i].SetToEliteStage(1f, 0.025f * dir);
            }
        }
    }

    private void SetActiveUI(bool active)
    {
        DEFINE.MainCanvas.gameObject.SetActive(active);
        DEFINE.PlayerTrm.gameObject.SetActive(active);
    }

    private IEnumerator RotateDirectingCoroutine()
    {
        SetActiveUI(false);
        DEFINE.PlayerTrm.position += Vector3.up * 3f;

        yield return new WaitForSeconds(startDelayTime / 10f);

        CameraManager.Instance.ActiveCamera(CameraType.DirectingCam);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        yield return StartCoroutine(RotateCoroutine(RotateCount));

        yield return new WaitForSeconds(0.5f);

        CameraManager.Instance.ActiveCamera(CameraType.MapCam);
        CameraManager.Instance.SetProjection(true);

        yield return new WaitForSeconds(1.8f);

        CameraManager.Instance.ActiveCamera(CameraType.MainCam);

        yield return new WaitForSeconds(1.75f);

        SetActiveUI(true);
    }

    #region Mid-Boss

    private IEnumerator MidBossDirectingBegin()
    {
        SetActiveUI(false);
        DEFINE.PlayerTrm.position += Vector3.up * 3f;

        yield return new WaitForSeconds(startDelayTime / 10f);

        CameraManager.Instance.ActiveCamera(CameraType.DirectingCam);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        midBossParticle.Simulate(0);
        midBossParticle.Play();

        yield return StartCoroutine(RotateCoroutine(RotateCount));
    }

    private IEnumerator MidBossDirectingMid()
    {
        yield return new WaitForSeconds(0.5f);

        CameraManager.Instance.ActiveCamera(CameraType.MapCam);
        CameraManager.Instance.SetProjection(true);

        yield return new WaitForSeconds(1.8f);

        CameraManager.Instance.ActiveCamera(CameraType.MainCam);

        yield return new WaitForSeconds(1.75f);

        CameraManager.Instance.SetProjection(false);
        CameraManager.Instance.ActiveCamera(CameraType.MidBossCam);
        CameraManager.Instance.GetCamera(CameraType.MidBossCam).gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
    }

    private IEnumerator MidBossDirectingEnd()
    {
        CameraManager.Instance.ActiveCamera(CameraType.MainCam);

        yield return new WaitForSeconds(1.5f);

        CameraManager.Instance.GetCamera(CameraType.MidBossCam).gameObject.SetActive(false);

        CameraManager.Instance.SetProjection(true);

        SetActiveUI(true);
    }

    private IEnumerator PrepareMidBossStageSequence()
    {
        yield return StartCoroutine(MidBossDirectingBegin());

        cube.gameObject.SetActive(false);

        BiomeFlags randBiome = (BiomeFlags)Random.Range(0, (int)BiomeFlags.End);
        MidBossCube midBossCube = null;
        while (preparedCube.TryGetPreparedCube(randBiome, out midBossCube) == false)
            randBiome = (BiomeFlags)Random.Range(0, (int)BiomeFlags.End);

        midBossCube.SetActive(true);

        yield return StartCoroutine(MidBossDirectingMid());

        yield return StartCoroutine(MidBossDirectingEnd());

        midBossCube.ActiveBoss(true);
    }

    #endregion

    private IEnumerator PrepareNewStageSequence()
    {
        yield return RotateDirecting();

        cube.SetActiveBridge(true);

        yield return new WaitUntil(() => stageChanged);

        stageChanged = false;
        StartStage();
    }

    public void StartGame()
    {
        DEFINE.MainCanvas.gameObject.SetActive(false);
        DEFINE.PlayerTrm.position = new Vector3(-22.5f, 147.5f, 15f);

        stageChanged = true;

        EndStage();
    }

    public void EndStage()
    {
        if (cube.ActivatedCells != null && cube.ActivatedCells.Count > 0)
        {
            Debug.Log("asd");
            for (int i = 0; i < cube.ActivatedCells.Count; i++)
                cube.ActivatedCells[i].ClearEliteStage(1f);
        }

        stageProgress++;
        if (stageProgress % (midBossTrigger + 1) == 0)
            StartCoroutine(PrepareMidBossStageSequence());
        else
            StartCoroutine(PrepareNewStageSequence());
    }

    public void EndMidBossStage()
    {
        preparedCubes.Find("DesertCube").gameObject.SetActive(false);
        cube.gameObject.SetActive(true);

        EndStage();
    }

    public void StartStage()
    {
        CubeCell currentCell = cube.GetCurrentCell();

        //SpawnEnemy(currentCell, 1);

        List<CubeCell> neighbors = new List<CubeCell>();
        List<CubeCell> newNeighbors = new List<CubeCell>();

        newNeighbors = CheckNeighbors(neighbors, currentCell.NeighborCells);
        while(newNeighbors.Count > 0)
            newNeighbors = CheckNeighbors(neighbors, newNeighbors);

        if(neighbors.Count <= 0)
            neighbors.Add(currentCell);

        foreach (CubeCell neighborCell in neighbors)
            SpawnEnemy(neighborCell, 1);

        cube.SetActiveBridge(false);
    }

    private List<CubeCell> CheckNeighbors(List<CubeCell> neighbors, List<CubeCell> newNeighbors)
    {
        List<CubeCell> tempNeighbors = new List<CubeCell>();

        for (int i = 0; i < newNeighbors.Count; i++)
        {
            foreach (CubeCell cell in newNeighbors[i].NeighborCells)
            {
                if (neighbors.Contains(cell) == false)
                {
                    tempNeighbors.Add(cell);
                    neighbors.Add(cell);
                }
            }
        }

        return tempNeighbors;
    }

    public void SpawnEnemy(CubeCell cell, int count)
    {
        for (int i = 0; i < count; i++)
            enemyList.Add(enemyFactory.SpawnEnemy(cell.CellBiome, cell.CellIndex));
    }

    public void RemoveEnemy(AIBrain enemy)
    {
        enemyList.Remove(enemy);

        if (enemyList.Count <= 0)
            EndStage();
    }
}
