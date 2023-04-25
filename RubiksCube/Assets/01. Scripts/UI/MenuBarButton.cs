using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuBarButton : MonoBehaviour
{
    [SerializeField] float backwardPosX;
    [SerializeField] float forwardPosX;
    [SerializeField] float activePosX;
    [SerializeField] float enactivePosX;

    private RectTransform rectTrm = null;
    private Button slideButton = null;

    private bool isActived = false;
    private bool onTweening = false;
    
    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        slideButton = transform.Find("SlideButton").GetComponent<Button>();

        isActived = false;
        onTweening = false;
    }

    public void Slide()
    {        
        Sequence seq = DOTween.Sequence();

        slideButton.interactable = false;
        onTweening = true;

        slideButton.transform.localScale = Vector3.one * (isActived ? 1 : -1);

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
}
