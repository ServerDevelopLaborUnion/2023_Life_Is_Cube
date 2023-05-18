using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private PlayerStat playerStat = null;
    private List<ItemDataSO> itemDatas = new List<ItemDataSO>();
    private List<ItemEvent> itemEvents = new List<ItemEvent>();

    private ItemInfoPanel itemInfoPanel = null;
    private InventoryUI inventoryUI = null;

    private void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    private void Start()
    {
        inventoryUI = UIManager.Instance.InventoryPanel.GetComponent<InventoryUI>();
        itemInfoPanel = UIManager.Instance.ItemInfoPanel;
    }

    private void Update()
    {
        itemEvents.ForEach(e => e.UpdateEvent());
    }

    public void AddItem(ItemDataSO itemData, List<ItemEvent> itemEvents)
    {
        itemDatas.Add(itemData);
        itemInfoPanel.Active(itemData);

        itemData.ItemStats.ForEach(s => { 
            playerStat.ModifyStat(s.statType, s.value); 
        });
        itemEvents.ForEach(e => {
            e.InitEvent();
            this.itemEvents.Add(e);
        });

        inventoryUI.SetItem(itemData);
    }

    public void ConsumeItem()
    {
        Debug.Log("아이템 소비됨");
        //소비
    }
}
