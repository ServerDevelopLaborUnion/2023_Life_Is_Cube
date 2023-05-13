using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOverlapCircle : MonoBehaviour
{
    [SerializeField] float radius = 20f;

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, radius, DEFINE.EnemyLayer);
            Debug.Log(enemies[0]);
        }
    }

    #if UNITY_EDITOR
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
    #endif
}
