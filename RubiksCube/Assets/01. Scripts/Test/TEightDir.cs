using UnityEngine;
using static DEFINE;

public class TEightDir : MonoBehaviour
{
    [SerializeField] DirectionFlags axisInfo;
    [SerializeField] Vector3 forward;
    [SerializeField] Vector3 upward;

    private void Start()
    {
        SetBlocksOfAxis();

        // transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
    }

    public void SetBlocksOfAxis()
    {
        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;

        for(int i = 0; i < 8; ++i)
        {
            ray.direction = Quaternion.AngleAxis(45f * i, upward) * forward;
            if (Physics.Raycast(ray, out hit, 10f, CubeLayer))
                hit.transform.SetParent(transform);
        }
    }

#if UNITY_EDITOR

    // forward, (0, 0, 1), (0, 0, 0)
    // right, (1, 0, 0), (0, 90, 0)
    // backward, (0, 0, -1), (0, 180, 0)
    // left, (-1, 0, 0), (0, 270, 0)

    private void OnDrawGizmos()
    {
        if(forward.sqrMagnitude <= 0 || upward.sqrMagnitude <= 0)
            return;

        Gizmos.color = Color.red;
        
        Ray ray = new Ray(transform.position, forward);

        for (int i = 0; i < 8; ++i)
        {
            ray.direction = Quaternion.AngleAxis(45f * i, upward) * forward;
            Gizmos.DrawRay(ray);
        }

        Gizmos.DrawWireSphere(transform.position, 1f);
    }

#endif
}
