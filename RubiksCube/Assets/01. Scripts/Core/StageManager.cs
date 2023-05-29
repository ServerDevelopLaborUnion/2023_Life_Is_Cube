using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] List<DirectionFlagList> ignoreAxes;

    [field : SerializeField] public int RotateCount { get; set; }
    [SerializeField] float startDelayTime = 1.8f;

    private Cube cube;
    private EnemyFactory enemyFactory;
    private List<AIBrain> enemyList = new List<AIBrain>();

    private bool stageChanged = false;
    public bool StageChanged { get => stageChanged; set => stageChanged = value; }
    
    [SerializeField] int midBossTrigger = 3;
    private int stageProgress = 0;

    private void Awake()
    {
        cube = GameObject.Find("Cube").GetComponent<Cube>();
        enemyFactory = cube.GetComponent<EnemyFactory>();
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

        // for(int i = 0; i < cube.ActivatedCells.Count; i++)
        // {
        //     Vector3[] pushOrders = cube.ActivatedCells[i].CheckNeighborCell();

        //     foreach(Vector3 dir in pushOrders)
        //         cube.ActivatedCells[i].SetToEliteStage(1f, 0.025f * dir);
        // }

        // yield return new WaitForSeconds(1f);
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

    private IEnumerator PrepareNewStageSequence()
    {
        yield return RotateDirecting();

        cube.SetActiveBridge(true);

        yield return new WaitUntil(() => stageChanged);

        stageChanged = false;
        StartStage();
    }

    private IEnumerator PrepareMidBossStageSequence()
    {
        yield return null;
        //한 면 맞추기
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
        if(stageProgress % midBossTrigger == 0)
            StartCoroutine(PrepareMidBossStageSequence());
        else
            StartCoroutine(PrepareNewStageSequence());
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
