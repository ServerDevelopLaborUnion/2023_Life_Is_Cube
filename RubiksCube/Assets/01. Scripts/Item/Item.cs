using System.Net;
using System.Collections;
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

    public void PopAnimation(float popDistance) => StartCoroutine(PopCoroutine(popDistance));

    private IEnumerator PopCoroutine(float popDistance, float popSpeed = 10f)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position - Vector3.up * popDistance;
        Vector2 randFactor = RandomOnUnitCircle() * popDistance;
        endPos.x += randFactor.x;
        endPos.z += randFactor.y;

        float popTime = Mathf.Max(0.3f, Vector3.Distance(startPos, endPos)) / popSpeed;
        float v0 = (endPos.y - startPos.y) + -Physics.gravity.y;
        float theta = 0f;
        float timer = 0f;

        //endPos.y += v0;

        while(theta < 1f)
        {
            timer += Time.deltaTime;
            theta = timer / popTime;

            Vector3 pos = Vector3.Lerp(startPos, endPos, theta);
            pos.y = startPos.y + (v0 * theta) - (-Physics.gravity.y * theta * theta);
            transform.position = pos;

            yield return null;
        }
    }

    private Vector2 RandomOnUnitCircle()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
