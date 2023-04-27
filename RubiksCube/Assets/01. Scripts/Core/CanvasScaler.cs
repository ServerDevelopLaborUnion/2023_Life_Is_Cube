using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(Screen.width * 2.66666666f, Screen.height * 2.66666666f);
        Debug.Log(new Vector2(Screen.width, Screen.height));
    }
}
