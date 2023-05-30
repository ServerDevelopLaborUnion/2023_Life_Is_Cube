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
    private EnemyFactory enemyFactory;
    private List<AIBrain> enemyList = new List<AIBrain>();
    private Transform preparedCubes = null;

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
        preparedCubes = GameObject.Find("PreparedCubes").transform;
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
        int currentCell = cube.GetCurrentCell().CellIndex;

        for (int i = 0; i < cnt;)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);

            if (ignoreAxes.Count > currentCell)
            {
                if (ignoreAxes[currentCell].flags.Contains((DirectionFlags)axis) == false)
                {
                    i++;
                    yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
                }
            }
        }

        cube.SortCellIndexes();
    }

    private IEnumerator RotateDirectingCoroutine()
    {
        DEFINE.MainCanvas.gameObject.SetActive(false);
        DEFINE.PlayerTrm.gameObject.SetActive(false);
        DEFINE.PlayerTrm.position += Vector3.up * 3f;

        yield return new WaitForSeconds(startDelayTime / 10f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, true);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        yield return StartCoroutine(RotateCoroutine(RotateCount));

        yield return new WaitForSeconds(0.5f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, true);
        CameraManager.Instance.SetProjection(true);

        yield return new WaitForSeconds(1.8f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMainCam, true);

        yield return new WaitForSeconds(1.75f);

        DEFINE.PlayerTrm.gameObject.SetActive(true);
        DEFINE.MainCanvas.gameObject.SetActive(true);
        UIManager.Instance.HPPanel.SetActive(true);
    }

    #region Mid-Boss

    private IEnumerator MidBossDirectingBegin()
    {
        DEFINE.MainCanvas.gameObject.SetActive(false);
        DEFINE.PlayerTrm.gameObject.SetActive(false);
        DEFINE.PlayerTrm.position += Vector3.up * 3f;

        yield return new WaitForSeconds(startDelayTime / 10f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, true);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        midBossParticle.Simulate(0);
        midBossParticle.Play();

        yield return StartCoroutine(RotateCoroutine(RotateCount));
    }

    private IEnumerator MidBossDirectingMid()
    {
        yield return new WaitForSeconds(0.5f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, true);
        CameraManager.Instance.SetProjection(true);

        yield return new WaitForSeconds(1.8f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMainCam, true);

        yield return new WaitForSeconds(1.75f);

        CameraManager.Instance.SetProjection(false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMainCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMidBossCam, true);
        CameraManager.Instance.CmMidBossCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
    }

    private IEnumerator MidBossDirectingEnd()
    {
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMidBossCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMainCam, true);

        yield return new WaitForSeconds(1.5f);

        CameraManager.Instance.CmMidBossCam.gameObject.SetActive(false);

        CameraManager.Instance.SetProjection(true);

        DEFINE.PlayerTrm.gameObject.SetActive(true);
        DEFINE.MainCanvas.gameObject.SetActive(true);
        UIManager.Instance.HPPanel.SetActive(true);
    }

    private IEnumerator PrepareMidBossStageSequence()
    {
        yield return StartCoroutine(MidBossDirectingBegin());

        cube.gameObject.SetActive(false);
        preparedCubes.Find("DesertCube").gameObject.SetActive(true);

        //한면만 맞춰진 프리팹 꺼내기

        yield return StartCoroutine(MidBossDirectingMid());

        //적 꺼내기

        yield return StartCoroutine(MidBossDirectingEnd());

        preparedCubes.Find("DesertCube/DesertMidBoss").GetComponent<AIBrain>().enabled = true;
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
        SpawnEnemyIntoCurrentCell(1);
        cube.SetActiveBridge(false);
    }

    public void SpawnEnemyIntoCurrentCell(int count)
    {
        cube.SortCellIndexes();
        CubeCell currentCell = cube.GetCurrentCell();

        for (int i = 0; i < count; i++)
            enemyList.Add(enemyFactory.SpawnEnemy(currentCell.CellBiome, currentCell.CellIndex));
    }

    public void RemoveEnemy(AIBrain enemy)
    {
        enemyList.Remove(enemy);

        if (enemyList.Count <= 0)
            EndStage();
    }
}
