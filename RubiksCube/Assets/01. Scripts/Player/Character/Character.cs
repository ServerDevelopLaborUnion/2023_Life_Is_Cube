using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// hihi hihi 
public class Character : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string className;
    public List<StatData> stats;

    [SerializeField] private Image icon;

    private CharacterSelectPanel panel;

    private void Awake()
    {
        panel = transform.parent.GetComponent<CharacterSelectPanel>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        panel.Current = this;
    }
}