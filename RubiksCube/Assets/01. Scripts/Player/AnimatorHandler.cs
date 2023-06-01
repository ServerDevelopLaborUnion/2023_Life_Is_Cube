using System;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public event Action OnAnimationEndTrigger = null;
    public event Action OnAnimationEventTrigger = null;

    private Animator animator = null;

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int onAttackHash = Animator.StringToHash("OnAttack");
    private readonly int onSpecialAttackHash = Animator.StringToHash("OnSpecialAttack"); //이거 바꿔야 됨
    private readonly int onRolling = Animator.StringToHash("OnRolling");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed) => animator.SetFloat(speedHash, speed);
    public void ToggleAttack(bool value) => animator.SetBool(onAttackHash, value);
    public void ToggleSpecialAttack(bool value) => animator.SetBool(onAttackHash, value);
    public void ToggleRolling(bool value) => animator.SetBool(onRolling, value);

    public void OnAnimationEnd() => OnAnimationEndTrigger?.Invoke();
    public void OnAnimationEvent() => OnAnimationEventTrigger?.Invoke();
}