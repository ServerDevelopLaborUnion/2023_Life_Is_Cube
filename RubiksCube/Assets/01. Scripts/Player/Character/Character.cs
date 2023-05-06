using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerDownHandler
{
    public List<StatData> stats;

    public void OnPointerDown(PointerEventData eventData)
    {
        CharacterSelectPanel.instance.Current = this;
    }
}