using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectHandler : PoolableMono
{
    [SerializeField] List<ParticleSystem> particles = null;
    [SerializeField] List<VisualEffect> effects = null;

    public override void Reset()
    {
        StopEffects();
    }

    public void PlayEffects(float playTime = 1f)
    {
        particles?.ForEach(p => p.Play());
        effects?.ForEach(e => e.Play());

        StartCoroutine(DelayCoroutine(playTime, () => {
            StopEffects();
            PoolManager.Instance.Push(this);
        }));
    }

    public void StopEffects()
    {
        particles?.ForEach(p => {
            p.Stop();
            p.Simulate(0);
        });

        effects?.ForEach(e => {
            e.Stop();
            e.Simulate(0);
        });
    }

    private IEnumerator DelayCoroutine(float delayTime, Action callback = null)
    {
        yield return new WaitForSeconds(delayTime);
        callback?.Invoke();
    }
}
