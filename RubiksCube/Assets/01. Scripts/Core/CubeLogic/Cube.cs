using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] bool startingRotate = false;
    [SerializeField] float rotateDuration = 1f;
    [SerializeField] float startDelay = 1f;

    private Dictionary<DirectionFlags, CubeAxis> cubeAxesDictionary;

    private void Awake()
    {
        List<CubeAxis> cubeAxes = new List<CubeAxis>();
        transform.GetComponentsInChildren<CubeAxis>(cubeAxes);
    
        cubeAxesDictionary = new Dictionary<DirectionFlags, CubeAxis>();
        cubeAxes.ForEach(axis => {
            if(cubeAxesDictionary.ContainsKey(axis.AxisInfo))
            {
                Debug.LogWarning("Current Axis of Cube Axis Already Existed on Dictionary");
                return;
            }

            cubeAxesDictionary.Add(axis.AxisInfo, axis); 
        });
    }

    private IEnumerator Start()
    {
        if(startingRotate == false)
            yield break;
            
        yield return new WaitForSeconds(startDelay);

        for(int i = 0; i < 10; ++ i)
        {
            int axis = Random.Range(0, (int)DirectionFlags.End);
            cubeAxesDictionary[(DirectionFlags)axis].SetBlocksOfAxis();
            // cubeAxesDictionary[(DirectionFlags)axis].Rotate(rotateDuration, (i % 2) == 0);

            yield return cubeAxesDictionary[(DirectionFlags)axis].Rotate(rotateDuration, (i % 2) == 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
