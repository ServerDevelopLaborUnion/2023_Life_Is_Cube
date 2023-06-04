using System.Collections;
using System.Collections.Generic;
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

    private List<AIBrain> enemyList = new List<AIBrain>();
    private List<CubeCell> neighbors = new List<CubeCell>();
    public List<CubeCell> Neighbors => neighbors;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                while (enemyList.Count > 0)
                    enemyList[0].GetComponent<IDamageable>().OnDamage(100000f, Vector3.zero, Vector3.zero);
            }
        }
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
                cube.Bridges.SetActiveBridge(i, dir.normalized, false);
                cube.ActivatedCells[i].SetToEliteStage(1f, 0.025f * dir);
            }
        }

        CubeCell currentCell = cube.GetCurrentCell();
        FindAllNeighbors(currentCell);
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

        // midBossParticle.Simulate(0);
        // midBossParticle.Play();

        // yield return StartCoroutine(RotateCoroutine(RotateCount));
        yield return cube.ReleaseCube();
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

        // BiomeFlags randBiome = (BiomeFlags)Random.Range(0, (int)BiomeFlags.End);
        BiomeFlags randBiome = BiomeFlags.Desert;
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

        cube.Bridges.SetActiveBridge(true);

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
        cube.Bridges.CurrentCellIdx = cube.GetCurrentCell().CellIndex;

        if (cube.ActivatedCells != null && cube.ActivatedCells.Count > 0)
        {
            for (int i = 0; i < cube.ActivatedCells.Count; i++)
                cube.ActivatedCells[i].ClearEliteStage(1f);
        }

        cube.Bridges.ClearBridgeActive();

        stageProgress++;
        if (stageProgress % (midBossTrigger + 1) == 0)
            StartCoroutine(PrepareMidBossStageSequence());
        else
            StartCoroutine(PrepareNewStageSequence());
    }

    public void EndMidBossStage()
    {
        preparedCube.RemoveCube(preparedCube.ActivedCubeBiome);
        cube.gameObject.SetActive(true);

        EndStage();
    }

    public void StartStage()
    {
        //CubeCell currentCell = cube.GetCurrentCell();

        //SpawnEnemy(currentCell, 1);

        CubeCell currentCell = cube.GetCurrentCell();
        FindAllNeighbors(currentCell);

        foreach (CubeCell neighborCell in neighbors)
            SpawnEnemy(neighborCell, 1);

        cube.Bridges.SetActiveBridge(false);
    }

    private void FindAllNeighbors(CubeCell currentCell)
    {
        neighbors.Clear();
        List<CubeCell> newNeighbors = new List<CubeCell>();

        newNeighbors = CheckNeighbors(neighbors, currentCell.NeighborCells);
        while (newNeighbors.Count > 0)
            newNeighbors = CheckNeighbors(neighbors, newNeighbors);

        if (neighbors.Count <= 0)
            neighbors.Add(currentCell);
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
        {
            // if (stageProgress % (midBossTrigger + 1) == 0)
            //     EndMidBossStage();
            // else 
                EndStage();
        }
    }
}
