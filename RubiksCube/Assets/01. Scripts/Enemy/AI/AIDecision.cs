using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIBrain aiBrain;

    public bool IsReverse = false;

    public virtual void SetUp(Transform parentRoot)
    {
        aiBrain = parentRoot.GetComponent<AIBrain>();
    }

    public abstract bool MakeDecision();
}
