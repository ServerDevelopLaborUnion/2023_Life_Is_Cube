using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemDataSO itemData;
    private Image itemImage = null;
    public bool Empty => (itemData == null);

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        RemoveItem();
    }

    public void SetItem(ItemDataSO item)
    {
        itemData = item;

        itemImage.color = Color.white;
        itemImage.sprite = itemData.ItemSprite;
    }

    public void RemoveItem()
    {
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.sprite = null;
        itemData = null;
    }
}
