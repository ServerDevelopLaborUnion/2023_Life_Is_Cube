using System.Transactions;
using UnityEngine;

public class TIntroCube : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] float lowestSimilar = 0.95f;

    private bool selected = false;
    private Transform cameraTrm = null;
    private Quaternion targetDir = Quaternion.identity;
    private DirectionFlags targetAxis = DirectionFlags.End;

    private void Awake()
    {
        cameraTrm = DEFINE.MainCam.transform;
        targetDir = Quaternion.LookRotation(-cameraTrm.forward);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = DEFINE.MainCam.ScreenPointToRay(Input.mousePosition);
            selected = Physics.Raycast(ray, out RaycastHit hit, DEFINE.MainCam.farClipPlane, DEFINE.CellLayer);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;

            float highestSimilar = lowestSimilar;

            CompareRotation(transform.up, DirectionFlags.Up, ref highestSimilar);
            CompareRotation(-transform.forward, DirectionFlags.Back, ref highestSimilar);
            CompareRotation(-transform.right, DirectionFlags.Left, ref highestSimilar);

            if (highestSimilar <= lowestSimilar)
                targetAxis = DirectionFlags.End;
        }

        RotateCube();
    }

    private void CompareRotation(Vector3 normal, DirectionFlags axis, ref float highestSimilar)
    {
        float dot = Quaternion.Dot(targetDir, Quaternion.LookRotation(normal));

        if (dot >= highestSimilar)
        {
            highestSimilar = dot;
            targetAxis = axis;
        }
    }

    private void RotateCube()
    {
        if (selected)
        {
            float xFactor = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            float yFactor = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            transform.Rotate(Vector3.up, -xFactor);
            transform.Rotate(Vector3.forward - Vector3.right, -yFactor);
        }
        else
        {
            if(targetAxis == DirectionFlags.End)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10f);
                return;
            }

            Vector3 fromDir = Vector3.zero;
            switch (targetAxis)
            {
                case DirectionFlags.Up:
                    fromDir = transform.up;
                    break;
                case DirectionFlags.Left:
                    fromDir = -transform.right;
                    break;
                case DirectionFlags.Back:
                    fromDir = -transform.forward;
                    break;
            }

            Vector3 dir = -cameraTrm.forward;
            Quaternion targetRotation = Quaternion.FromToRotation(fromDir, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation * transform.rotation, Time.deltaTime * 10f);
        }
    }
}
