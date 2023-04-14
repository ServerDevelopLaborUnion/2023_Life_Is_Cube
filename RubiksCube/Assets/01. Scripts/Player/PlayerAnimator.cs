using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{   
    public event Action OnAnimationEndTrigger = null;
    public event Action OnAnimationEventTrigger = null;

    private Animator animator = null;

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int onAttackHash = Animator.StringToHash("OnAttack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed) => animator.SetFloat(speedHash, speed);
    public void ToggleAttack(bool value) => animator.SetBool(onAttackHash, value);

    public void OnAnimationEnd() => OnAnimationEndTrigger?.Invoke();
    public void OnAnimationEvent() => OnAnimationEventTrigger?.Invoke();
}
