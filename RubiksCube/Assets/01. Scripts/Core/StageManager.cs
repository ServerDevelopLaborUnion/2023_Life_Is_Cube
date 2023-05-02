using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    [SerializeField] int startRotateCount = 15;
    [SerializeField] float startDelayTime = 1.8f;

    private Cube cube;
    private EnemyFactory enemyFactory;

    private void Awake()
    {
        cube = GameObject.Find("Cube").GetComponent<Cube>();
        enemyFactory = cube.GetComponent<EnemyFactory>();
    }

    private void Start()
    {
        StartGame();
    }

    private IEnumerator StartDirectingCoroutine()
    {
        yield return new WaitForSeconds(startDelayTime / 10f);
        
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, true);
        CameraManager.Instance.SetProjection(false);

        yield return new WaitForSeconds(startDelayTime);

        for (int i = 0; i < startRotateCount; i++)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);
            yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
        }
        //yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        //회전 끝났을 때

        CubeCell[] cells = cube.SortIdexesOnCells();
        for(int i = 0; i < 9; i++)
        {
            if(cells.Length <= i || cells[i] == null)
                continue;
            enemyFactory.SpawnImmediately(30, cells[i].CellBiome, cells[i].CellIndex);
        }

        //적 소환 끝났을 때

        yield return new WaitForSeconds(0.5f);

        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, true);

        CameraManager.Instance.SetProjection(true);

        yield return new WaitForSeconds(1.8f);

        
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, false);
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMainCam, true);

        yield return new WaitForSeconds(1.5f);

        DEFINE.PlayerTrm.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        DEFINE.PlayerTrm.gameObject.SetActive(false);
        DEFINE.PlayerTrm.position = new Vector3(-22.5f, 147.5f, 15f);

        StartCoroutine(StartDirectingCoroutine());
    }
}
