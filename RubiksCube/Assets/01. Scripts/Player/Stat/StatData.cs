using UnityEngine;

[System.Serializable]
public struct StatData
{
    public StatFlags statType;
    public float defaultValue;
    public float minValue;
    public float maxValue;
    public float value;

    public StatData ModifyStat(float degree) 
    {
        value = Mathf.Clamp(value + degree, minValue, maxValue);
        return this;
    }

    public StatData SetStat(float value) 
    { 
        this.value = Mathf.Clamp(value, minValue, maxValue);
        return this;
    }
}