using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerDownHandler
{
    public List<StatData> stats;
    private CharacterSelectPanel current;

    private void Awake()
    {
        current = transform.parent.parent.GetComponent<CharacterSelectPanel>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        current.Current = this;
    }
}