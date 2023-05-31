using UnityEngine;

public class MidBossCube : MonoBehaviour
{  
    [SerializeField] AIBrain bossAI;

    public void ActiveBoss(bool value) => bossAI.enabled = value;
    public void SetActive(bool value) => gameObject.SetActive(value);
}
