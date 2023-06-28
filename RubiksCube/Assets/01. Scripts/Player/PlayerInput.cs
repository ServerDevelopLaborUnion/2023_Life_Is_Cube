using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask detectLayer;

    public event Action<Vector3> OnMovementKeyPressed = null;
    public event Action OnRollingKeyPressed = null;
    public event Action OnConsumeKeyPressed = null;
    public event Action OnAttackKeyPressed = null;
    public event Action OnInteractKeyPressed = null;
    public event Action OnSpecialAttackKeyPressed = null;
    public event Action OnSpecialAttack2KeyPressed = null;

    private Vector3 input;

    private void Update()
    {
        //ConsumeInput();

        MovementInput();
        AttackInput();
        SpecialAttackInput();
        SpecialAttack2Input();
        InteractInput();
        RollingInput();
    }

    public void MovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        input = new Vector3(x, 0, y);
        
        OnMovementKeyPressed?.Invoke(input);
    }

    public void RollingInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnRollingKeyPressed?.Invoke();
    }

    public void AttackInput()
    {
        if(Input.GetMouseButtonDown(0))
            OnAttackKeyPressed?.Invoke();
    }

    public void SpecialAttackInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            OnSpecialAttackKeyPressed?.Invoke();
    }

    public void SpecialAttack2Input()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
            OnSpecialAttack2KeyPressed?.Invoke();
    }

    public void InteractInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
            OnInteractKeyPressed?.Invoke();
    }

    public Vector3 GetInputDirection() => input;
}
