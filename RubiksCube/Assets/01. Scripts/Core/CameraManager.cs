using System.Collections.Generic;
using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera activedCam = null;
    private Dictionary<CameraType, CinemachineVirtualCamera> cmCamMap = null;

    private CinemachineBasicMultiChannelPerlin mainCamPerlin;

    private void Awake()
    {
        Transform cmCamsTrm = GameObject.Find("CmCams").transform;
        cmCamMap = new Dictionary<CameraType, CinemachineVirtualCamera>();

        foreach (CameraType type in typeof(CameraType).GetEnumValues())
        {
            if (cmCamMap.ContainsKey(type))
                continue;

            CinemachineVirtualCamera cmCam = cmCamsTrm.Find($"Cm{type.ToString()}")?.GetComponent<CinemachineVirtualCamera>();
            if (cmCam == null)
                continue;

            cmCam.m_Priority = 0;
            cmCamMap.Add(type, cmCam);
        }

        mainCamPerlin = cmCamMap[CameraType.MainCam].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ActiveCamera(CameraType.MainCam);
    }

    public void ActiveCamera(CameraType type)
    {
        if (activedCam != null)
            activedCam.m_Priority = 5;

        if (cmCamMap.ContainsKey(type) == false)
            return;

        activedCam = cmCamMap[type];
        activedCam.m_Priority = 15;
    }

    public CinemachineVirtualCamera GetCamera(CameraType type) => cmCamMap[type];

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