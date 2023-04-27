using UnityEngine;

public class TIntroCube : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5f;

    private bool isSelected = false;
    private bool isSide = false;
    private Transform cube;
    private Vector2 startPos;
    private Vector2 lastPos;

    private void Awake()
    {
        cube = transform.Find("Cube");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = DEFINE.MainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            isSelected = Physics.Raycast(ray, out hit, DEFINE.MainCam.farClipPlane, DEFINE.CellLayer);

            if(isSelected)
            {
                isSide = (hit.transform.name != "Top");
                lastPos = Input.mousePosition;
            }
        }

        if(isSelected)
        {
            float xFactor = (isSide ? (lastPos.x - Input.mousePosition.x) : 0) * Time.deltaTime * rotateSpeed;
            float yFactor = (isSide ? 0 : (lastPos.y - Input.mousePosition.y)) * Time.deltaTime * rotateSpeed;
            cube.Rotate(-yFactor, xFactor, 0, Space.World);

            lastPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            if(isSide == false)
            {
                cube.Rotate()
            }
        }
    }
}
