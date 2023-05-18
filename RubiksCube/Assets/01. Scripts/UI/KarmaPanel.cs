using TMPro;
using UnityEngine;

public class KarmaPanel : MonoBehaviour
{
    private TextMeshProUGUI karmaText;

    private void Awake()
    {
        karmaText = transform.Find("BG/Text").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayKarma(int value)
    {
        karmaText.SetText(value.ToString());
    }
    
    public void SetActiveToggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
