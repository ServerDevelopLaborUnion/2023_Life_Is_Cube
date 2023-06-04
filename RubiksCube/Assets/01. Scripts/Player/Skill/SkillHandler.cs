using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    [Header("!직업 선택창 순서랑 맞추기!")]
    [SerializeField] List<Skill> skills = new List<Skill>();

    [SerializeField] private Skill skill1;
    [SerializeField] private Skill skill2;

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
