using UnityEngine;
using static DEFINE;

public abstract class State : MonoBehaviour, IState
{
    protected PlayerMovement playerMovement = null;
    protected PlayerInput playerInput = null;
    protected AnimatorHandler playerAnimator = null;
    protected StateHandler stateHandler = null;
    protected JoyStick joyStick = null;

    public abstract void OnStateEnter();
    public abstract void StateUpdate();
    public abstract void OnStateExit();

    public virtual void SetUp(Transform root)
    {
        playerMovement = root.GetComponent<PlayerMovement>();
        playerInput = root.GetComponent<PlayerInput>();
        stateHandler = root.GetComponent<StateHandler>();
        playerAnimator = root.Find("Model").GetComponent<AnimatorHandler>();
        joyStick = MainCanvas.GetChild(0).GetChild(0).GetChild(0).GetComponent<JoyStick>();
    }
}
