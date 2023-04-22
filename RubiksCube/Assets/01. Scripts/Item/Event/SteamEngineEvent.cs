using UnityEngine;

public class SteamEngineEvent : ItemEvent
{
    [SerializeField] ParticleSystem smokeParticle;

    public override void ActiveEvent()
    {
    }

    public override void InitEvent()
    {
        //smokeParticle?.Play();
    }

    public override void UpdateEvent()
    {
    }
}
