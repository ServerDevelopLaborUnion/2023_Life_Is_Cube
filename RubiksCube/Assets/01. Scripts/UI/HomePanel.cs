using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void GoToHome()
    {
        SceneLoader.Instance.LoadSceneAsync("TitleScene");
    }
}
