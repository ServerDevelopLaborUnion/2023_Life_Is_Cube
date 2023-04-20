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

    // up => forward, up
    // down => forward, -up
    // left => up, -right
    // right => up, right
    // forward => up, forward
    // back => up, -forward

    public void SetBlocksOfAxis()
    {
        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;

        for(int i = 0; i < 8; ++i)
        {
            ray.direction = Quaternion.AngleAxis(45f * i, upward) * forward;
            // Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(45f * i, upward) * forward * 1000f, Color.green, 0.1f);
            
            if (Physics.Raycast(ray, out hit, CubeLayer))
                hit.transform.SetParent(transform);
        }

        Debug.Log(transform.childCount);
    }

    public void UnsetBlocksOfAxis()
    {
        while(transform.childCount > 0)
            transform.GetChild(0).SetParent(transform.parent);
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
        float ratio = 0f;

        while(timer < rotateDuration)
        {
            ratio = timer / rotateDuration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, ratio);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        UnsetBlocksOfAxis();
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
