using UnityEngine;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class StatPanel : MonoBehaviour
{
    [SerializeField] float activePosY = 40f;
    [SerializeField] float enactivePosY = -360f;
    
    private RectTransform rectTrm = null;
    private bool isActived;

    private Dictionary<StatFlags, StatInfo> statInfoDictionary = new Dictionary<StatFlags, StatInfo>();

    private void Awake()
    {
        rectTrm = transform.GetComponent<RectTransform>();
        Transform statPanel = DEFINE.MainCanvas.Find("StatPanel");
        
        foreach(StatFlags stat in typeof(StatFlags).GetEnumValues())
        {
            StatInfo statInfo = statPanel.Find(stat.ToString())?.GetComponent<StatInfo>();
            if(statInfo != null)
                statInfoDictionary.Add(stat, statInfo);
        }
    }

    public void DisplayStat(StatFlags stat, float currentValue, float maxValue)
    {
        if(statInfoDictionary.ContainsKey(stat))
            statInfoDictionary[stat]?.DisplayStat(currentValue, maxValue);
    }

    public void Slide()
    {
        Sequence seq = DOTween.Sequence().SetUpdate(true);

        seq.Append(rectTrm.DOAnchorPosY(isActived ? enactivePosY : activePosY, 0.2f).SetEase(Ease.Linear));
        seq.AppendCallback(() => {

            isActived = !isActived;
            seq.Kill();
        });
    }
}
