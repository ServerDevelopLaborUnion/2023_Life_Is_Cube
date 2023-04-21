using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask detectLayer;

    public event Action<Vector3> OnMovementKeyPressed = null;
    public event Action OnConsumeKeyPressed = null;
    public event Action OnAttackKeyPressed = null;

    private void Update()
    {
        ConsumeInput();
        //AttackInput();
    }

    public void MovementInput(Vector2 input)
    {
        // float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");

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
    
    public Vector3 GetMouseWorldPosition() 
    {
        Ray ray = DEFINE.MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, DEFINE.MainCam.farClipPlane, detectLayer);

        return (result ? hit.point : Vector3.zero);
    }
}
