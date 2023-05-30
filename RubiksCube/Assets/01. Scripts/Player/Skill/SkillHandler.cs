using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    [Header("!직업 선택창 순서랑 맞추기!")]
    [SerializeField] List<Skill> skills = new List<Skill>();
    
    [SerializeField] private Skill currentSkill;

    public bool TrySkill()
    {
        return currentSkill.TrySpecialAttack();
    }

    public void SetSkill(int idx)
    {
        currentSkill = skills[idx];
    }
}
