using System.Collections;
using UnityEngine;

public class TKnifeAnimation : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    private bool ableAttack = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ableAttack = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ableAttack)
            {
                ableAttack = false;   
                animator.SetBool("OnSlash", true);
            }
        }
    }

    public void OnAnimationEnd()
    {
        animator.SetBool("OnSlash", false);
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(0.105f);
        ableAttack = true;
    }
}
