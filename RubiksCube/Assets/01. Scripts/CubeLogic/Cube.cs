using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float rotateDuration = 1f;

    private Dictionary<DirectionFlags, CubeAxis> cubeAxesDictionary;
    private CubeCell currentCell = null;
    private Bridge bridge = null;

    private void Awake()
    {
        List<CubeAxis> cubeAxes = new List<CubeAxis>();
        transform.GetComponentsInChildren<CubeAxis>(cubeAxes);

        cubeAxesDictionary = new Dictionary<DirectionFlags, CubeAxis>();
        cubeAxes.ForEach(axis =>
        {
            if (cubeAxesDictionary.ContainsKey(axis.AxisInfo))
            {
                Debug.LogWarning("Current Axis of Cube Axis Already Existed on Dictionary");
                return;
            }

            cubeAxesDictionary.Add(axis.AxisInfo, axis);
        });

        bridge = transform.Find("Bridges").GetComponent<Bridge>();
    }

    public CubeAxis GetCubeAxis(DirectionFlags axis) => cubeAxesDictionary[axis];

    public Coroutine RotateAroundAxis(DirectionFlags axis, bool clockWise = true)
    {
        return StartCoroutine(RotateAroundAxisCoroutine(axis, clockWise));
    }

    private IEnumerator RotateAroundAxisCoroutine(DirectionFlags axis, bool clockWise = true)
    {
        cubeAxesDictionary[axis].SetBlocksOfAxis();
        yield return cubeAxesDictionary[axis].Rotate(rotateDuration, clockWise);
        yield return new WaitForSeconds(0.001f);
    }

    public CubeCell GetCurrentCell()
    {
        if(Physics.Raycast(DEFINE.PlayerTrm.position, Vector3.down, out RaycastHit hit, 1000f, DEFINE.CellLayer))
        {
            currentCell = hit.collider.GetComponent<CubeCell>();
            Debug.Log(currentCell);
        }
        
        return currentCell;
    }

    public CubeCell[] SortCellIndexes()
    {
        List<CubeCell> cells = new List<CubeCell>();

        CubeAxis topAxis = cubeAxesDictionary[DirectionFlags.Up];
        topAxis.SetBlocksOfAxis();

        RaycastHit hit;
        CubeCell cell;

        for (int i = 0; i < topAxis.ChildTrm.childCount; i++)
        {
            if (Physics.Raycast(topAxis.ChildTrm.GetChild(i).position, Vector3.up, out hit, 100000, DEFINE.CellLayer))
            {
                if (hit.transform.TryGetComponent<CubeCell>(out cell))
                {
                    if(i == 3)
                        cell?.ModifyCellIndex(5);
                    else if(i == 7)
                        cell?.ModifyCellIndex(3);
                    else if(3 < i && i < 7) //4 or 5 or 6
                        cell?.ModifyCellIndex(12 - i);
                    else 
                        cell?.ModifyCellIndex(i);
                    
                    cells.Add(cell);
                }
            }
        }

        if (Physics.Raycast(topAxis.ChildTrm.position, Vector3.up, out hit, 100000, DEFINE.CellLayer))
        {
            if (hit.transform.TryGetComponent<CubeCell>(out cell))
            {
                cell?.ModifyCellIndex(4);
                cells.Add(cell);
            }
        }

        topAxis.UnsetBlocksOfAxis();

        cells.Sort((a, b) => a.CellIndex - b.CellIndex);
        return cells.ToArray();
    }

    public void SetActiveBridge(bool value)
    {
        bridge.SetActiveBridge(value);
    }
}
