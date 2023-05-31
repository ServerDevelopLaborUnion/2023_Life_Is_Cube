using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[SerializeField, Tooltip("The distance from the center the stick able to move")] float maxDistance = 10f;
    [SerializeField, Tooltip("the speed at which the stick returns when the drag is finished")] float returningSpeed = 50f;

    [Header("Events")]
    [SerializeField] UnityEvent<Vector2> OnValueChagned;
    [SerializeField] UnityEvent OnDragStart;
    [SerializeField] UnityEvent OnDragEnd;

    private float maxDistance = 10f;

    private Vector3 center = Vector3.zero;
    public Vector3 Center => center;

    private bool onDragged = false;
    public bool OnDragged => onDragged;

    public Vector3 lastDir = Vector3.forward;

    private void Start()
    {
        center = transform.position;

        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();
        maxDistance = (parentRect.rect.xMax - parentRect.rect.xMin) * Screen.width / 1920f * 0.5f;
    }

    private void Update()
    {
        if(onDragged == false)
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.unscaledDeltaTime * returningSpeed);
        
        OnValueChagned?.Invoke(GetJoyStickValue());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDragged = true;
        OnDragStart?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if((transform.position - Input.mousePosition).magnitude < 300)
            transform.position = Input.mousePosition;

        if((transform.position - center).magnitude > maxDistance)
        {
            Vector3 normalFactor = GetJoyStickValue();
            transform.position = center + normalFactor * maxDistance;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lastDir = GetJoyStickValue();
        onDragged = false;
        OnDragEnd?.Invoke();;
    }

    public Vector2 GetJoyStickValue() => ((onDragged == false) ? Vector2.zero : (transform.position - center).normalized);
}
