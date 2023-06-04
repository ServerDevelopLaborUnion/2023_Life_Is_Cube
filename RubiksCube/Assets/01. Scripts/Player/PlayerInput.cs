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

    private void Update()
    {
        //ConsumeInput();

        //AttackInput();
        #if UNITY_EDITOR
        MovementInput_Dev();
        #endif
    }

    public void MovementInput(Vector2 input)
    {
        Vector3 dir = new Vector3(input.x, 0, input.y);

        OnMovementKeyPressed?.Invoke(dir);
    }

    public void MovementInput_Dev()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        Vector3 input = new Vector3(x, 0, y);
        
        if(input.sqrMagnitude > 0)
            OnMovementKeyPressed?.Invoke(input);
    }

    public void RollingInput()
    {
        OnRollingKeyPressed?.Invoke();
    }

    private void ConsumeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnConsumeKeyPressed?.Invoke();
    }

    public void AttackInput()
    {
        //if(Input.GetMouseButtonDown(0))
        OnAttackKeyPressed?.Invoke();
    }

    public void SpecialAttackInput()
    {
        OnSpecialAttackKeyPressed?.Invoke();
    }

    public void InteractInput()
    {
        OnInteractKeyPressed?.Invoke();
    }
}
