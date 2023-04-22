using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MS {
    public float a;
}

[System.Serializable]
public class B {
    public MS ms;
}

public class TStruct : MonoBehaviour
{
    public List<MS> mss;

    private void Awake()
    {
        MS s = mss[0];//.a = 10;
        s.a = 10f;
        mss[0] = s;
    }
}
