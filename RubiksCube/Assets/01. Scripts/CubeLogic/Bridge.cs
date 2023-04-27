using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private List<Transform> bridges;

    private void Awake()
    {
        bridges = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
            bridges.Add(transform.GetChild(i));
    }

    public void ActiveBridge()
    {
        bridges.ForEach(b => b.gameObject.SetActive(true));
    }

    public void InactiveBridge()
    {
        bridges.ForEach(b => b.gameObject.SetActive(false));
    }
}
