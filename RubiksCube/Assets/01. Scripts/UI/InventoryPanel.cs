using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] float activePosX = 0f;
    [SerializeField] float enactivePosX = -1080f;

    private RectTransform rectTrm = null;
    private GameObject spotlightCamTrm = null;
    private Button exitButton = null;

    private bool isActived = false;
    private bool onTweening = false;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        spotlightCamTrm = DEFINE.PlayerTrm.Find("SpotlightCam").gameObject;
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        isActived = false;
        onTweening = false;
    }

    public void Slide()
    {
        Sequence seq = DOTween.Sequence().SetUpdate(true);

        onTweening = true;
        spotlightCamTrm.gameObject.SetActive(!isActived);
        UIManager.Instance.InputPanel.gameObject.SetActive(isActived);

        if(isActived)
            exitButton.gameObject.SetActive(false);

        seq.Append(rectTrm.DOAnchorPosX(isActived ? enactivePosX : activePosX, 0.3f).SetEase(Ease.Linear));
        seq.AppendCallback(() => {
            onTweening = false;

            if(isActived == false)
                exitButton.gameObject.SetActive(true);


            isActived = !isActived;
            seq.Kill();
        });
    }
}
