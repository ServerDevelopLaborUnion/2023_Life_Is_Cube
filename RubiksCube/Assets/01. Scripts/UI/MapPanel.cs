using Cinemachine;
using UnityEngine;

public class MapPanel : MonoBehaviour
{
    private CinemachineVirtualCamera mapVCam = null;
    private GameObject exitButton = null;
    private bool isActived = false;

    private void Awake()
    {
        mapVCam = GameObject.Find("MapVCam").GetComponent<CinemachineVirtualCamera>();
        exitButton = transform.Find("ExitButton").gameObject;
    }

    public void AppearMap()
    {
        mapVCam.Priority = (isActived ? 5 : 15);
        UIManager.Instance.InputPanel.gameObject.SetActive(isActived);
        exitButton.SetActive(!isActived);

        isActived = !isActived;
    }
}
