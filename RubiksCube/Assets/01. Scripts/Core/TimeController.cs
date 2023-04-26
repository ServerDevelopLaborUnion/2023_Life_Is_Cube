using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance = null;

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    public void ModifyTimeScale(float timeScale, float standbyTime, Action callback = null)
    {
        StartCoroutine(TimeScaleCoroutine(timeScale, standbyTime, callback));
    }

    private IEnumerator TimeScaleCoroutine(float timeScale, float standbyTime, Action callback)
    {
        yield return new WaitForSecondsRealtime(standbyTime);
        Time.timeScale = timeScale;

        callback?.Invoke();
    }
}
