using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera cmMainCam = null;
    public CinemachineVirtualCamera CmMainCam => cmMainCam;
    private CinemachineVirtualCamera cmMapCam = null;
    public CinemachineVirtualCamera CmMapCam => cmMapCam;
    private CinemachineVirtualCamera cmDirectingCam = null;
    public CinemachineVirtualCamera CmDirectingCam => cmDirectingCam;

    private CinemachineBasicMultiChannelPerlin mainCamPerlin;

    public CameraManager()
    {
        cmMainCam = GameObject.Find("CmMainCam")?.GetComponent<CinemachineVirtualCamera>();
        cmMapCam = GameObject.Find("CmMapCam")?.GetComponent<CinemachineVirtualCamera>();
        cmDirectingCam = GameObject.Find("CmDirectingCam")?.GetComponent<CinemachineVirtualCamera>();

        mainCamPerlin = cmMainCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetActiveCam(CinemachineVirtualCamera cam, bool value) => cam.m_Priority = (value ? 15 : 5);
    public void SetProjection(bool isOrtho) => DEFINE.MainCam.orthographic = isOrtho;

    /// <param name="time">시간</param>
    /// <param name="amplitude">진폭</param>
    /// <param name="frequency">진동수</param>
    public void ShakeCam(float time, float amplitude, float frequency)
    {
        mainCamPerlin.m_AmplitudeGain = amplitude;
        mainCamPerlin.m_FrequencyGain = frequency;

        StartCoroutine(DelayCoroutine(time, () => ShakeCam(amplitude, frequency)));
    }

    /// <param name="amplitude">진폭</param>
    /// <param name="frequency">진동수</param>
    public void ShakeCam(float amplitude, float frequency)
    {
        mainCamPerlin.m_AmplitudeGain = amplitude;
        mainCamPerlin.m_FrequencyGain = frequency;
    }

    private IEnumerator DelayCoroutine(float delay, Action callback = null, bool realTime = false)
    {
        if (realTime)
            yield return new WaitForSecondsRealtime(delay);
        else
            yield return new WaitForSeconds(delay);

        callback?.Invoke();
    }
}