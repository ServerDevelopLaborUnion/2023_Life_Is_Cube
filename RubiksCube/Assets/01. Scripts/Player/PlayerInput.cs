using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementKeyPressed = null;
    public event Action OnConsumeKeyPressed = null;
    public event Action OnAttackKeyPressed = null;

    private void Update()
    {
        MovementInput();
        ConsumeInput();
        AttackInput();
    }

    private void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0, v);

        OnMovementKeyPressed?.Invoke(input);
    }

    private void ConsumeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnConsumeKeyPressed?.Invoke();
    }

    private void AttackInput()
    {
        if(Input.GetMouseButtonDown(0))
            OnAttackKeyPressed?.Invoke();
    }
}
