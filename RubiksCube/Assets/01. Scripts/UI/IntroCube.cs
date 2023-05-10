using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCube : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] float lowestSimilar = 0.95f;

    [SerializeField] GameObject panel;

    private bool selected = false;
    private Camera cam = null;
    private Quaternion targetDir = Quaternion.identity;
    private DirectionFlags targetAxis = DirectionFlags.End;

    private Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        cam = DEFINE.MainCam;
        targetDir = Quaternion.LookRotation(-cam.transform.forward);

        // transform.Rotate(Vector3.forward - Vector3.right, 60);

        // Debug.Log(transform.rotation);
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
            {
                targetAxis = DirectionFlags.End;
                targetRotation = Quaternion.identity;
            }
            else
            {
                switch (targetAxis)
                {
                    case DirectionFlags.Up:
                        // fromDir = transform.up;
                        targetRotation = new Quaternion(-0.35355f, 0.00000f, 0.35355f, 0.86603f);
                        break;
                    case DirectionFlags.Left:
                        // fromDir = -transform.right;
                        targetRotation = new Quaternion(0.09905f, -0.36964f, -0.23912f, 0.89240f);
                        break;
                    case DirectionFlags.Back:
                        // fromDir = -transform.forward;
                        targetRotation = new Quaternion(0.23912f, 0.36964f, -0.09905f, 0.89240f);
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = DEFINE.MainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, cam.farClipPlane, DEFINE.CellLayer))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (targetAxis != DirectionFlags.End)
                {
                    switch (targetAxis)
                    {
                        case DirectionFlags.Up:
                            Debug.Log("Character SceneLoad");
                            //SceneManager.LoadScene("JaeHee");
                            break;
                        case DirectionFlags.Left:
                            SettingOnOff(true);
                            break;
                        case DirectionFlags.Back:

                            break;
                    }
                }
            }
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
            // targetRotation = Quaternion.FromToRotation(normal, -cameraTrm.forward) * transform.rotation;
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
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.Clamp(Time.deltaTime * 10f, 0, 1));
        // {
        // if (targetAxis == DirectionFlags.End)
        // {
        //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10f);
        //     return;
        // }

        // Vector3 dir = -cameraTrm.forward;
        // Quaternion targetRotation = Quaternion.FromToRotation(fromDir, dir);
        // }
    }

    public void SettingOnOff(bool value)
    {
        panel.SetActive(value);
    }

}
