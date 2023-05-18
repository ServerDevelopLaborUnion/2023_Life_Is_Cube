using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public List<Stat> ItemStats;
    public Sprite ItemSprite;
    public string ItemName;
    public string ItemDescription;
}
