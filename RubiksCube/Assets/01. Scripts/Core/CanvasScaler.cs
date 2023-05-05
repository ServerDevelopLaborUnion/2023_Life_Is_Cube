using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        Debug.Log(new Vector2(Screen.width, Screen.height));
    }
}
