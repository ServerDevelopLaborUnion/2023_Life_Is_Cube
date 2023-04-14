using UnityEngine;

[System.Serializable]
public struct StatData
{
    public StatFlags statType;
    public float defaultValue;
    public float minValue;
    public float maxValue;
    public float value;

    public void ModifyStat(float degree) => value = Mathf.Clamp(value + degree, minValue, maxValue);
    public void SetStat(float value) => this.value = Mathf.Clamp(value, minValue, maxValue);
}
