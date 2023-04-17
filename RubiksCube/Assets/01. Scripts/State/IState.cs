using UnityEngine;

public interface IState
{
    public void OnStateEnter();
    public void StateUpdate();
    public void OnStateExit();

    public void SetUp(Transform root);
}
