using UnityEngine;

public class MapPanel : MonoBehaviour
{
    private GameObject exitButton = null;
    private bool isActived = false;

    private void Awake()
    {
        exitButton = transform.Find("ExitButton").gameObject;
    }

    public void AppearMap()
    {
        CameraManager.Instance.SetActiveCam(CameraManager.Instance.CmMapCam, !isActived);
        UIManager.Instance.InputPanel.gameObject.SetActive(isActived);
        exitButton.SetActive(!isActived);

        isActived = !isActived;
    }
}
