using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class StatPanel : MonoBehaviour
{
    private TextMeshProUGUI karmaText;

    private Dictionary<StatFlags, TextMeshProUGUI> statTextDictionary = new Dictionary<StatFlags, TextMeshProUGUI>();

    private void Awake()
    {
        Transform statPanel = DEFINE.MainCanvas.Find("StatPanel");
        
        foreach(StatFlags stat in typeof(StatFlags).GetEnumValues())
        {
            TextMeshProUGUI statText = statPanel.Find(stat.ToString())?.Find("Text")?.GetComponent<TextMeshProUGUI>();
            if(statText != null)
                statTextDictionary.Add(stat, statText);
        }

        karmaText = statPanel.Find("Karma/Text")?.GetComponent<TextMeshProUGUI>();
    }

    public void DisplayKarma(int value)
    {
        karmaText.SetText(value.ToString());
    }

    public void DisplayStat(StatFlags stat, float value)
    {
        if(statTextDictionary.ContainsKey(stat))
            statTextDictionary[stat]?.SetText(value.ToString());
    }

    public void SetActiveToggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
