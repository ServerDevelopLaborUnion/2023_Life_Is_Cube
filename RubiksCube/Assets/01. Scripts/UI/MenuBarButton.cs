using UnityEngine;
using DG.Tweening;

public class MenuBarButton : MonoBehaviour
{
    [SerializeField] float backwardPosX;
    [SerializeField] float forwardPosX;
    [SerializeField] float activePosX;
    [SerializeField] float enactivePosX;

    private RectTransform rectTrm = null;

    private bool isActived = false;
    private bool onTweening = false;
    
    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();

        isActived = false;
        onTweening = false;
    }

    public void Slide()
    {
        Sequence seq = DOTween.Sequence();

        onTweening = true;
        seq.Append(rectTrm.DOLocalMoveX(isActived ? forwardPosX : backwardPosX, 0.2f).SetEase(Ease.InExpo));
        seq.Append(rectTrm.DOLocalMoveX(isActived ? backwardPosX : forwardPosX, 0.3f).SetEase(Ease.OutCubic));
        seq.Append(rectTrm.DOLocalMoveX(isActived ? enactivePosX : activePosX, 0.05f).SetEase(Ease.Linear));
        seq.AppendCallback(() => {
            isActived = !isActived;
            onTweening = false;
            seq.Kill();
        });
    }
}
