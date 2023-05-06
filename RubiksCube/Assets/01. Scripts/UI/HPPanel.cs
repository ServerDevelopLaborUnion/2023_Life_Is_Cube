using UnityEngine;
using UnityEngine.UI;

public class HPPanel : MonoBehaviour
{
    private Image hpImage = null;

    private void Awake()
    {
        hpImage = transform.Find("HPImage").GetComponent<Image>();
    }

    public void SetHP(float percent)
    {
        hpImage.fillAmount = Mathf.Max(0f, percent);
    }
}
