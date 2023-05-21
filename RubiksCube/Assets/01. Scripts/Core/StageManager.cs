using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] int rotateCount = 15;
    [SerializeField] float startDelayTime = 1.8f;

    private Cube cube;
    private EnemyFactory enemyFactory;
    private List<AIBrain> enemyList = new List<AIBrain>();

    private bool stageChanged = false;
    public bool StageChanged { get => stageChanged; set => stageChanged = value; }

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

    private IEnumerator RotateDirectingCoroutine()
    {
        DEFINE.MainCanvas.gameObject.SetActive(false);
        DEFINE.PlayerTrm.gameObject.SetActive(false);
        DEFINE.PlayerTrm.position += Vector3.up * 3f;

        yield return new WaitForSeconds(startDelayTime / 10f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, true);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        for (int i = 0; i < rotateCount; i++)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);
            yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
        }

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

    private IEnumerator StageEndSequence()
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
        StartCoroutine(StageEndSequence());
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
