using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected PlayerMovement playerMovement = null;
    protected PlayerInput playerInput = null;

    public abstract void OnStateEnter();
    public abstract void StateUpdate();
    public abstract void OnStateExit();

    public virtual void SetUp(Transform root)
    {
        playerMovement = root.GetComponent<PlayerMovement>();
        playerInput = root.GetComponent<PlayerInput>();
    }
}
