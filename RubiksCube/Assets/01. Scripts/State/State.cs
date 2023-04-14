using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected PlayerMovement playerMovement = null;
    protected PlayerInput playerInput = null;
    protected PlayerAnimator playerAnimator = null;
    protected StateHandler stateHandler = null;

    public abstract void OnStateEnter();
    public abstract void StateUpdate();
    public abstract void OnStateExit();

    public virtual void SetUp(Transform root)
    {
        playerMovement = root.GetComponent<PlayerMovement>();
        playerInput = root.GetComponent<PlayerInput>();
        stateHandler = root.GetComponent<StateHandler>();
        playerAnimator = root.Find("Model").GetComponent<PlayerAnimator>();
    }
}
