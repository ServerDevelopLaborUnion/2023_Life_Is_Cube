using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] float displayDuration = 3.5f;
    [SerializeField] float tweeningDuration = 0.85f;
    [SerializeField] float activePosY = -50f;
    [SerializeField] float enactivePosY = 250f;

    private Image itemImage;
    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private RectTransform rectTrm;

    private bool isActived = false;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        descriptionText = transform.Find("DescriptionText").GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        SlideDown(0f);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Active(ItemDataSO itemData)
    {
        itemImage.sprite = itemData.ItemSprite;
        nameText.SetText(itemData.ItemName);
        descriptionText.SetText(itemData.ItemDescription);

        rectTrm.DOKill();
        StopAllCoroutines();

        Active();
    }

    public void Active()
    {
        SlideDown(0f);

        Sequence seq = DOTween.Sequence();

        isActived = true;

        Vector3 pos = rectTrm.position;
        pos.y = enactivePosY;
        rectTrm.position = pos;
        
        seq.Append(rectTrm.DOAnchorPosY(activePosY, tweeningDuration).SetEase(Ease.OutQuart));
        seq.AppendCallback(() => {
            seq.Kill();
            StartCoroutine(DelayCoroutine(displayDuration, () => SlideDown(tweeningDuration)));
        });
    }

    private void SlideDown(float duration)
    {
        Sequence seq = DOTween.Sequence();
        
        seq.Append(rectTrm.DOAnchorPosY(enactivePosY, duration).SetEase(Ease.Linear));
        seq.AppendCallback(() => {
            isActived = false;
            seq.Kill();
        });
    }

    private IEnumerator DelayCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}
