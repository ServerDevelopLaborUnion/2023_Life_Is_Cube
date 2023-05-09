using UnityEngine;

public class HitFeedback : Feedback
{
    [SerializeField] EffectHandler hitParticle;
    [SerializeField] float effectPlayTime;

    private AIActionData aiActionData;

    private void Awake()
    {
        aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        
    }

    public override void FinishFeedback()
    {
        EffectHandler effect = PoolManager.Instance.Pop(hitParticle) as EffectHandler;
        effect.transform.position = aiActionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(aiActionData.HitNormal);

        effect.PlayEffects(effectPlayTime);
    }
}
