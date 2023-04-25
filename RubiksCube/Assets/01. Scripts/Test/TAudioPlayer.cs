using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAudioPlayer : MonoBehaviour
{
    [ContextMenu("클립 1")]
    public void PlayTestClip1()
    {
        SoundManager.Instance.PlayTestClip1();
    }

    [ContextMenu("클립 2")]
    public void PlayTestClip2() 
    {
        SoundManager.Instance.PlayTestClip2();
    }
}
