using System.Collections;
using System.Net;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    private Cube cube;

    private void Awake()
    {
        cube = GameObject.Find("Cube").GetComponent<Cube>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmDirectingCam, true);
            CameraManager.Instance.SetProjection(false);

            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(1.8f);

        for (int i = 0; i < 15; i++)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);
            yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
        }
    }

    public void StartGame()
    {
    }
}
