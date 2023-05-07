using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    [SerializeField] List<Item> itemList = new List<Item>();
    [SerializeField] float yFactor = 5f;

    private int cost = 10;

    private KarmaHandler karmaHandler = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnInteract(transform);
    }

    public void OnInteract(Transform performer)
    {
        if (performer.name != "Player")
            return;

        if (karmaHandler == null)
            karmaHandler = performer.GetComponent<KarmaHandler>();

        if (karmaHandler.TryUseKarma(cost))
        {

        }
        else
        {
            int randIdx = Random.Range(0, itemList.Count);

            if (randIdx >= itemList.Count)
                return;

            Item item = itemList[randIdx];
            item = PoolManager.Instance.Pop(item.name) as Item;

            item.transform.position = transform.position + Vector3.up * yFactor;
            item.PopAnimation(yFactor);
        }
    }
}
