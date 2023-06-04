using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHandler : MonoBehaviour
{
    [Header("!���� ����â ������ ���߱�!")]
    [SerializeField] List<Skill> skills = new List<Skill>();

    [SerializeField] private Skill skill1;
    [SerializeField] private Skill skill2;

    private Image skill1Image;
    private Image skill2Image;

    private void Start()
    {
        skill1Image = UIManager.Instance.InputPanel.Find("InteractButton/SpecialButton/FillImage").GetComponent<Image>();
        skill2Image = UIManager.Instance.InputPanel.Find("InteractButton/SpecialButton2/FillImage").GetComponent<Image>();
    }

    private void Update()
    {
        if (skill1Image != null)
            skill1Image.fillAmount = ((Time.time - skill1.latestSpecialAttackTime) / skill1.specialAttackCooldown);

        if (skill2Image != null)
            skill2Image.fillAmount = ((Time.time - skill2.latestSpecialAttackTime) / skill2.specialAttackCooldown);

    }

    public bool TrySkill1()
    {
        return skill1.TrySpecialAttack();
    }

    public bool TrySkill2()
    {
        return skill2.TrySpecialAttack();
    }

    public void SetSkill(int idx, int order = 1)
    {
        Debug.Log("SetSkill");
        if (order == 1)
        {
            skill1 = skills[idx];
        }
        if(order == 2)
        {
            skill2 = skills[idx];
        }
    }

}
