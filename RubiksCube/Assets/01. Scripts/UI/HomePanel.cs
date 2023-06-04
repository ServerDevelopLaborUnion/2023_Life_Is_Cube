using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void GoToHome()
    {
        TimeController.Instance.ResetTimeScale();
        SceneLoader.Instance.LoadSceneAsync("TitleScene");
    }
}
