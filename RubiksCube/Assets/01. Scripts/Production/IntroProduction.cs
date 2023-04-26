using System.Collections;
using UnityEngine;

public class IntroProduction : MonoBehaviour
{
    [SerializeField] Vector2 loopDelay = new Vector2(1.5f, 2.5f);
    [SerializeField] bool cameraProduction = false;
    
    private Transform cameraTrm = null;
    private Cube cube = null;

    private void Awake()
    {
        cameraTrm = GameObject.Find("CameraProduction").transform;
        cube = transform.Find("Cube").GetComponent<Cube>();
    }

    private void Start()
    {
        StartCoroutine(RotateCubeLoop());
    }

    private void Update()
    {
        if(cameraProduction)
            cameraTrm.Rotate(new Vector3(0, -15f * Time.deltaTime, 0));
    }

    private IEnumerator RotateCubeLoop()
    {
        while(true) {
            int axis = Random.Range(0, (int)DirectionFlags.End);
            yield return cube.RotateAroundAxis((DirectionFlags)axis, Random.Range(0, 2) == 0);
            yield return new WaitForSeconds(Random.Range(loopDelay.x, loopDelay.y));
        }
    }
}
