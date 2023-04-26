using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float rotateDuration = 1f;

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

    public Coroutine RotateAroundAxis(DirectionFlags axis, bool clockWise = true)
    {
        return StartCoroutine(RotateAroundAxisCoroutine(axis, clockWise));
    }

    private IEnumerator RotateAroundAxisCoroutine(DirectionFlags axis, bool clockWise = true)
    {
        cubeAxesDictionary[(DirectionFlags)axis].SetBlocksOfAxis();
        yield return cubeAxesDictionary[(DirectionFlags)axis].Rotate(rotateDuration, clockWise);
        yield return new WaitForSeconds(0.1f);
    }
}
