using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void GoToHome()
    {
        SceneLoader.Instance.LoadSceneAsync("TitleScene");
    }
}
