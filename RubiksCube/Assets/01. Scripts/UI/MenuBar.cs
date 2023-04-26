using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    [SerializeField] float backwardPosX;
    [SerializeField] float forwardPosX;
    [SerializeField] float activePosX;
    [SerializeField] float enactivePosX;

    private RectTransform rectTrm = null;
    private GameObject blockPanel = null;
    private Button slideButton = null;

    private bool isActived = false;
    private bool onTweening = false;
    
    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        slideButton = transform.Find("SlideButton").GetComponent<Button>();
        blockPanel = transform.Find("BlockPanel").gameObject;

        isActived = false;
        onTweening = false;
    }

    public void Slide(bool pause = false)
    {
        if(onTweening)
        {
            if(isActived)
                //if(pause)
                    TimeController.Instance.ModifyTimeScale(1, 0f);

            return;
        }

        Sequence seq = DOTween.Sequence().SetUpdate(true);

        slideButton.interactable = false;
        onTweening = true;

        slideButton.transform.localScale = Vector3.one * (isActived ? 1 : -1);

        if(pause)
            TimeController.Instance.ModifyTimeScale(isActived ? 1 : 0, 0f);

        seq.Append(rectTrm.DOAnchorPosX(isActived ? forwardPosX : backwardPosX, 0.15f).SetEase(Ease.InExpo));
        seq.Append(rectTrm.DOAnchorPosX(isActived ? backwardPosX : forwardPosX, 0.4f).SetEase(Ease.OutCubic));
        seq.Append(rectTrm.DOAnchorPosX(isActived ? enactivePosX : activePosX, 0.15f).SetEase(Ease.OutExpo));
        seq.AppendCallback(() => {
            onTweening = false;
            slideButton.interactable = true;

            isActived = !isActived;
            seq.Kill();
        });
    }

    public void Block(bool isBlock) => blockPanel.SetActive(isBlock);
}