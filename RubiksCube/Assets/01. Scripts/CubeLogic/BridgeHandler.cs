using System.Collections.Generic;
using UnityEngine;

public class BridgeHandler : MonoBehaviour
{
    private List<Dictionary<DirectionFlags, GameObject>> bridgeMapList;
    private Dictionary<Vector3, DirectionFlags> directionMap = new Dictionary<Vector3, DirectionFlags>()
    {
        [Vector3.right] = DirectionFlags.Right,
        [Vector3.left] = DirectionFlags.Left,
        [Vector3.forward] = DirectionFlags.Up,
        [Vector3.back] = DirectionFlags.Down
    };
    
    private GameObject innerConfiner;
    private Cube cube;

    private int currentCellIdx = 0;
    public int CurrentCellIdx { get => currentCellIdx; set => currentCellIdx = value; }

    private void Awake()
    {
        innerConfiner = transform.parent.Find("CubeConfiner").gameObject;

        bridgeMapList = new List<Dictionary<DirectionFlags, GameObject>>();
        for (int i = 0; i < 9; i++)
        {
            bridgeMapList.Add(new Dictionary<DirectionFlags, GameObject>());
            Transform bridgeCotainer = transform.Find($"Bridge{i}");

            foreach(DirectionFlags dirType in typeof(DirectionFlags).GetEnumValues())
            {
                Transform bridge = bridgeCotainer.Find($"Bridge{dirType.ToString()}");
                if(bridge != null)
                    bridgeMapList[i].Add(dirType, bridge.gameObject);
            }
        }

        cube = transform.parent.GetComponent<Cube>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetActiveBridge(bool value)
    {
        //bridges.ForEach(b => b.gameObject.SetActive(true));
        gameObject.SetActive(value);
        innerConfiner.SetActive(!value);
    }

    public void SetActiveBridge(int i, Vector3 dir, bool active)
    {
        if (directionMap.ContainsKey(dir) == false)
            return;

        if(bridgeMapList.Count < i)
            return;

        if(bridgeMapList[i].ContainsKey(directionMap[dir]) == false)
            return;

        bridgeMapList[i][directionMap[dir]].SetActive(active);
    }

    public void ClearBridgeActive()
    {
        foreach(Dictionary<DirectionFlags, GameObject> map in bridgeMapList)
            foreach(GameObject bridge in map.Values)
                bridge.SetActive(true);
    }

    public void TryChangeStage()
    {
        CubeCell currentCell = cube.GetCurrentCell();

        if(StageManager.Instance.Neighbors.Contains(currentCell) == false)
            if(currentCellIdx != currentCell.CellIndex)
                StageManager.Instance.StageChanged = true;
    }
}
