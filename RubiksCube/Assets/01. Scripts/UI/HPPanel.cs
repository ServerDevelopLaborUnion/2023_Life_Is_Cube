using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPPanel : MonoBehaviour
{
    private Transform hpBar = null;
    private Image slider = null;
    private TextMeshProUGUI hpText = null;

    private bool active = false;

    private void Awake()
    {
        hpBar = transform.Find("HPBar");
        slider = hpBar.Find("Slider").GetComponent<Image>();
        hpText = hpBar.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetActive(false);
    }

    private void FixedUpdate()
    {
        if(active)
            hpBar.position = DEFINE.MainCam.WorldToScreenPoint(DEFINE.PlayerTrm.position);
    }

    public void SetHP(float currentHP, float maxHP)
    {
        slider.fillAmount = Mathf.Max(0f, currentHP / maxHP);
        hpText.SetText($"{((int)currentHP).ToString()} / {((int)maxHP).ToString()}");
    }

    public void SetActive(bool value)
    {
        active = value;
        hpBar.gameObject.SetActive(value);
    }

    public void SetActiveToggle()
    {
        SetActive(!active);
    }
}
