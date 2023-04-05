using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float rotateSpeed = 8f;
    [SerializeField] float gravity = -9.81f;

    private CharacterController characterController = null;

    private Vector3 movementVelocity = Vector3.zero;
    public Vector3 MovementVelocity => movementVelocity;

    private float verticalVelocity = 0f;

    public bool IsActiveMove { get; set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if(IsActiveMove)
            CalculateMovement();
    
        if(characterController.isGrounded == false)
            verticalVelocity = gravity * Time.fixedDeltaTime;
        else
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;

        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);
    }

    public void SetMovementVelocity(Vector3 value)
    {
        movementVelocity = value;
    }

    public void SetRotation(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0f;

        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void CalculateMovement()
    {
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        movementVelocity *= moveSpeed * Time.fixedDeltaTime;
        if(movementVelocity.sqrMagnitude > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementVelocity), Time.deltaTime * rotateSpeed);
    }

    public void StopImmediatly()
    {
        movementVelocity = Vector3.zero;
    }
}
