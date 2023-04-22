using System.Collections.Generic;
using UnityEngine;

public class Item : PoolableMono, IInteractable
{
    [SerializeField] ItemDataSO itemData = null;
    private List<ItemEvent> itemEvents;

    private void Awake()
    {
        itemEvents = new List<ItemEvent>();
        GetComponents<ItemEvent>(itemEvents);
    }

    public void OnInteract(Transform performer)
    {
        //플레이어한테 아이템 넣는 코드
        performer.GetComponent<PlayerHand>().AddItem(itemData, itemEvents);

        gameObject.layer = 0;
        transform.Find("Model").gameObject.SetActive(false);
        GetComponent<Animator>().enabled = false;

        transform.SetParent(performer.Find("ItemArchive"));
        transform.localPosition = Vector3.zero;
    }

    public override void Reset()
    {
    }
}
