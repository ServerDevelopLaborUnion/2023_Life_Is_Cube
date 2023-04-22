using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] List<StatData> statDatas;
    private Dictionary<StatFlags, StatData> statDictionary = new Dictionary<StatFlags, StatData>();

    private void Awake()
    {
        foreach(StatData data in statDatas)
            statDictionary.Add(data.statType, data);

    }

    public StatData GetStatData(StatFlags statType) => statDictionary[statType];
    public float GetStat(StatFlags statType) => statDictionary[statType].value;
    public void ModifyStat(StatFlags statType, float degree) => statDictionary[statType] = statDictionary[statType].ModifyStat(degree);
    public void SetStat(StatFlags statType, float value) => statDictionary[statType] = statDictionary[statType].SetStat(value);
}
