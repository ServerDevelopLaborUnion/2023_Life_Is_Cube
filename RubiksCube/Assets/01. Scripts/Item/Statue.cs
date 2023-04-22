 using UnityEngine;

public class Statue : MonoBehaviour, IInteractable
{
    public void OnInteract(Transform performer)
    {
        Debug.Log("옛다 아이템 받아라");
    }
}
