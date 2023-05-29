using System.Collections;
using UnityEngine;
using static DEFINE;

public class CubeAxis : MonoBehaviour
{
    [Header("Axis Info")]
    [SerializeField] DirectionFlags axisInfo;
    public DirectionFlags AxisInfo => axisInfo;

    [SerializeField] Vector3 forward;
    [SerializeField] Vector3 upward;

    private Transform childTrm = null;
    public Transform ChildTrm => childTrm;

    // up => forward, up
    // down => forward, -up
    // left => up, -right
    // right => up, right
    // forward => up, forward
    // back => up, -forward

    private void Awake()
    {
        childTrm = transform.Find("ChildBlocks");
    }

    public void SetBlocksOfAxis()
    {
        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;

        for(int i = 0; i < 8; ++i)
        {
            ray.direction = Quaternion.AngleAxis(45f * i, upward) * Quaternion.AngleAxis(-45, upward) * forward; // upward 축의 왼쪽 위부터 돌릴 거임
            // Debug.DrawLine(transform.positions, transform.position + Quaternion.AngleAxis(45f * i, upward) * forward * 1000f, Color.green, Time.deltaTime);
            
            if (Physics.Raycast(ray, out hit, CubeLayer))
                hit.transform.SetParent(childTrm);
        }

        //Debug.Log(transform.childCount);
    }

    public void UnsetBlocksOfAxis()
    {
        while(childTrm.childCount > 0)
            childTrm.GetChild(0).SetParent(transform.parent);
    }

    public Coroutine Rotate(float rotateDuration, bool clockWise = true)
    {
        return StartCoroutine(RotateCoroutine(rotateDuration, clockWise));
    }

    private IEnumerator RotateCoroutine(float rotateDuration, bool clockWise)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(90f * (clockWise ? 1 : -1), upward);
        float timer = 0f;
        float theta = 0f;

        while(timer < rotateDuration)
        {
            theta = timer / rotateDuration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, theta);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    // #if UNITY_EDITOR

    // forward, (0, 0, 1), (0, 0, 0)
    // right, (1, 0, 0), (0, 90, 0)
    // backward, (0, 0, -1), (0, 180, 0)
    // left, (-1, 0, 0), (0, 270, 0)

    // private void OnDrawGizmosSelected()
    // {
    //     if(forward.sqrMagnitude <= 0 || upward.sqrMagnitude <= 0)
    //         return;

    //     Gizmos.color = Color.red;
        
    //     for (int i = 0; i < 8; ++i)
    //         Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(45f * i, upward) * forward * 10000f);

    //     // Gizmos.DrawWireSphere(transform.position, 10000f);
    // }

    // #endif
}
