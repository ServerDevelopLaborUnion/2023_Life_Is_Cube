using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private PlayerStat playerStat = null;
    private List<ItemDataSO> itemDatas = new List<ItemDataSO>();
    private List<ItemEvent> itemEvents = new List<ItemEvent>();

    private void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        itemEvents.ForEach(e => e.UpdateEvent());
    }

    public void AddItem(ItemDataSO itemData, List<ItemEvent> itemEvents)
    {
        itemDatas.Add(itemData);

        itemData.itemStats.ForEach(s => { 
            playerStat.ModifyStat(s.statType, s.value); 
        });
        itemEvents.ForEach(e => {
            e.InitEvent();
            this.itemEvents.Add(e);
        });
    }

    public void ConsumeItem()
    {
        Debug.Log("아이템 소비됨");
        //소비
    }
}
