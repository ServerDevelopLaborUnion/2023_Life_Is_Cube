using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] List<StatData> statDatas;
    private Dictionary<StatFlags, StatData> statDictionary = new Dictionary<StatFlags, StatData>();
    private StatPanel statPanel = null;

    private void Awake()
    {
        foreach(StatData data in statDatas)
            statDictionary.Add(data.statType, data);

        statPanel = DEFINE.MainCanvas.Find("StatPanel")?.GetComponent<StatPanel>();
    }

    private void Start()
    {
        foreach(StatFlags stat in typeof(StatFlags).GetEnumValues())
            if(stat == StatFlags.MaxHP)
                continue;
            else if(statDictionary.ContainsKey(stat))
                statPanel?.DisplayStat(stat, statDictionary[stat].value);
    }

    public StatData GetStatData(StatFlags statType) => statDictionary[statType];
    public float GetStat(StatFlags statType) => statDictionary[statType].value;
    public void ModifyStat(StatFlags statType, float degree)
    {
        statDictionary[statType] = statDictionary[statType].ModifyStat(degree);
        statPanel?.DisplayStat(statType, statDictionary[statType].value);
    }
    public void SetStat(StatFlags statType, float value)
    {
        statDictionary[statType] = statDictionary[statType].SetStat(value);
        statPanel?.DisplayStat(statType, statDictionary[statType].value);
    }
}
