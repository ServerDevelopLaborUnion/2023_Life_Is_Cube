using System;
using System.Collections;
using UnityEngine;

public class SpecialAttack2State : State
{
    public override void OnStateEnter()
    {
        playerAnimator.ToggleSpecialAttack(true);
        playerMovement.StopImmediatly();

        // playerMovement.IsActiveRotate = false;

        // Vector3 lookTarget = playerInput.GetMouseWorldPosition();

        // if(lookTarget.sqrMagnitude <= 0)
        //     lookTarget = transform.forward + transform.position;

        // playerMovement.SetRotation(lookTarget);

        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
    }

    public override void StateUpdate()
    {

    }

    public override void OnStateExit()
    {

        //playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        playerAnimator.ToggleAttack(false);
        StartCoroutine(DelayCoroutine(0.15f, () => stateHandler.ChangeState(StateFlags.Normal)));
    }

    private IEnumerator DelayCoroutine(float delay, Action callback = null)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
