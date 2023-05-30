using System.Collections.Generic;
using UnityEngine;

public class BridgeHandler : MonoBehaviour
{
    private List<Transform> bridges;
    private GameObject innerConfiner;
    private Cube cube;

    private int currentCellIdx = 0;
    public int CurrentCellIdx => currentCellIdx;

    private void Awake()
    {
        innerConfiner = transform.parent.Find("InnerConfiner").gameObject;
        
        bridges = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
            bridges.Add(transform.GetChild(i));
        
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

        currentCellIdx = cube.GetCurrentCell().CellIndex;
    }

    public void TryChangeStage()
    {
        StageManager.Instance.StageChanged = (currentCellIdx != cube.GetCurrentCell().CellIndex);
    }
}
