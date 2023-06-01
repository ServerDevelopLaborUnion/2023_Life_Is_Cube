using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float rotateDuration = 1f;

    private Dictionary<DirectionFlags, CubeAxis> cubeAxesDictionary;
    private List<CubeCell> activatedCells;
    public List<CubeCell> ActivatedCells => activatedCells;
    private CubeCell currentCell = null;
    private BridgeHandler bridges = null;

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

        bridges = transform.Find("Bridges").GetComponent<BridgeHandler>();
    }

    public CubeAxis GetCubeAxis(DirectionFlags axis) => cubeAxesDictionary[axis];

    public Coroutine RotateAroundAxis(DirectionFlags axis, bool clockWise = true)
    {
        return StartCoroutine(RotateAroundAxisCoroutine(axis, clockWise));
    }

    private IEnumerator RotateAroundAxisCoroutine(DirectionFlags axis, bool clockWise = true)
    {
        cubeAxesDictionary[axis].SetBlocksOfAxis();
        if (cubeAxesDictionary[axis].ChildTrm.childCount != 8)
            yield return null;
        else
            yield return cubeAxesDictionary[axis].Rotate(Random.Range(rotateDuration - 0.09f, rotateDuration + 0.1f), clockWise);

        yield return null;
        cubeAxesDictionary[axis].UnsetBlocksOfAxis();
        // yield return new WaitForSeconds(0.0f);
    }

    public CubeCell GetCurrentCell()
    {
        if (Physics.Raycast(DEFINE.PlayerTrm.position, Vector3.down, out RaycastHit hit, 1000f, DEFINE.CellLayer))
            currentCell = hit.collider.GetComponent<CubeCell>();

        return currentCell;
    }

    public CubeCell[] SortCellIndexes()
    {
        activatedCells = new List<CubeCell>();

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
                    if (i == 3)
                        cell?.ModifyCellIndex(5);
                    else if (i == 7)
                        cell?.ModifyCellIndex(3);
                    else if (3 < i && i < 7) //4 or 5 or 6
                        cell?.ModifyCellIndex(12 - i);
                    else
                        cell?.ModifyCellIndex(i);

                    activatedCells.Add(cell);
                }
            }
        }

        if (Physics.Raycast(topAxis.ChildTrm.position, Vector3.up, out hit, 100000, DEFINE.CellLayer))
        {
            if (hit.transform.TryGetComponent<CubeCell>(out cell))
            {
                cell?.ModifyCellIndex(4);
                activatedCells.Add(cell);
            }
        }

        topAxis.UnsetBlocksOfAxis();

        activatedCells.Sort((a, b) => a.CellIndex - b.CellIndex);
        return activatedCells.ToArray();
    }

    public void SetActiveBridge(bool value)
    {
        bridges.SetActiveBridge(value);
    }
}
