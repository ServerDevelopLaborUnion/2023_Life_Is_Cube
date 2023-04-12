using UnityEngine;

public class ConsumingState : State
{
    private PlayerHand playerHand = null;

    public override void OnStateEnter()
    {
        playerHand.ConsumeItem();
        stateHandler.ChangeState(StateFlags.Normal);
    }

    public override void StateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void SetUp(Transform root)
    {
        base.SetUp(root);

        playerHand = root.GetComponent<PlayerHand>();
    }
}
