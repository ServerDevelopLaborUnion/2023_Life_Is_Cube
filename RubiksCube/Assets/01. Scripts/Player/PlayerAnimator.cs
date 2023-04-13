using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{   
    private Animator animator = null;

    private readonly int speedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)  {animator.SetFloat(speedHash, speed);Debug.Log(speed);} 
}
