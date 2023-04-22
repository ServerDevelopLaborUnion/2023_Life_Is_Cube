using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask detectLayer;

    public event Action<Vector3> OnMovementKeyPressed = null;
    public event Action OnConsumeKeyPressed = null;
    public event Action OnAttackKeyPressed = null;
    public event Action OnInteractKeyPressed = null;

    private void Update()
    {
        //ConsumeInput();

        //AttackInput();
    }

    public void MovementInput(Vector2 input)
    {
        Vector3 dir = new Vector3(input.x, 0, input.y);

        OnMovementKeyPressed?.Invoke(dir);
    }

    private void ConsumeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnConsumeKeyPressed?.Invoke();
    }

    public void AttackInput()
    {
        //if(Input.GetMouseButtonDown(0))
        OnAttackKeyPressed?.Invoke();
    }

    public void InteractInput()
    {
        OnInteractKeyPressed?.Invoke();
    }
}
