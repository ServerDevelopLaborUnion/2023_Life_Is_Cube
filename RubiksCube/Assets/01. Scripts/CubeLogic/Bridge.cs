using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private List<Transform> bridges;
    private GameObject innerConfiner;

    private void Awake()
    {
        innerConfiner = transform.parent.Find("InnerConfiner").gameObject;
        
        bridges = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
            bridges.Add(transform.GetChild(i));
    }

    public void SetActiveBridge(bool value)
    {
        //bridges.ForEach(b => b.gameObject.SetActive(true));
        gameObject.SetActive(value);
        innerConfiner.SetActive(!value);
    }
}
