using Cinemachine;
using UnityEngine;

public class CameraManager
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera cmMainCam = null;
    public CinemachineVirtualCamera CmMainCam => cmMainCam;
    private CinemachineVirtualCamera cmMapCam = null;
    public CinemachineVirtualCamera CmMapCam => cmMapCam;
    private CinemachineVirtualCamera cmDirectingCam = null;
    public CinemachineVirtualCamera CmDirectingCam => cmDirectingCam;
    
    public CameraManager()
    {
        cmMainCam = GameObject.Find("CmMainCam")?.GetComponent<CinemachineVirtualCamera>();
        cmMapCam = GameObject.Find("CmMapCam")?.GetComponent<CinemachineVirtualCamera>();
        cmDirectingCam = GameObject.Find("CmDirectingCam")?.GetComponent<CinemachineVirtualCamera>();
    }

    public void SetActiveCam(CinemachineVirtualCamera cam, bool value) => cam.m_Priority = (value ? 15 : 5);
    public void SetProjection(bool isOrtho) => DEFINE.MainCam.orthographic = isOrtho;
}