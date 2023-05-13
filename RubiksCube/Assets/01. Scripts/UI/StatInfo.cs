using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatInfo : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image slider;

    private void Awake()
    {
        slider = transform.Find("Slide").GetComponent<Image>();
        text = transform.Find("Value").GetComponent<TextMeshProUGUI>();
    }    

    public void DisplayStat(float currentValue, float maxValue)
    {
        slider.fillAmount = currentValue / maxValue;
        text.SetText(currentValue.ToString());        
    }
}
