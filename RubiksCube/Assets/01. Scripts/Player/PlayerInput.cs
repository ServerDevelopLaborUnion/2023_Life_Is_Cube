using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnMovementKeyPressed = null;

    private void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(h, v);

        OnMovementKeyPressed?.Invoke(input);
    }
}
