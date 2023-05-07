using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [SerializeField] bool active = false;

    private void Awake()
    {
        if(active == false)
            return;

        GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        Debug.Log(new Vector2(Screen.width, Screen.height));
    }
}
