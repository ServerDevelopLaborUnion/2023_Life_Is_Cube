using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// hihi hihi 
public class Character : MonoBehaviour, IPointerDownHandler
{
    [Header("Class Name")]
    public string className = string.Empty;
    public List<StatData> stats;

    [Header("Icons")]
    public Sprite classIcon = null;
    public Sprite skillIcon = null;

    [Header("Skill")]
    public string skillName = string.Empty;
    public string skillDescription = string.Empty;

    public CharacterSelectPanel panel = null;

    private void Awake()
    {
        panel = transform.parent.GetComponent<CharacterSelectPanel>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        panel.Current = this;
    }
}