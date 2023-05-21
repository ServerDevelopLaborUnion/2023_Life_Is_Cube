using UnityEngine;

public class HitFeedback : Feedback
{
    [SerializeField] EffectHandler hitParticle;
    [SerializeField] float effectPlayTime;

    private AIActionData aiActionData;

    private void Awake()
    {
        aiActionData = transform.parent.GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        EffectHandler effect = PoolManager.Instance.Pop(hitParticle) as EffectHandler;
        // Debug.Log(aiActionData.HitPoint);
        effect.transform.position = aiActionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(aiActionData.HitNormal);

        effect.PlayEffects(effectPlayTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
