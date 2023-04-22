using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DEFINE;

public class InteractButton : MonoBehaviour
{
    [SerializeField] Transform performer = null;

    [SerializeField] Sprite attackButtonSprite;
    [SerializeField] Sprite interactButtonSprite;

    public UnityEvent OnAttackEvent;
    public UnityEvent OnInteractEvent;

    private Button button = null;
    private bool isInteractState = false;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        isInteractState = false;
        button.image.sprite = attackButtonSprite;
        button.onClick.AddListener(AttackEvent);
    }

    private void Update()
    {
        DetectInteractableObjects();
    }

    private void DetectInteractableObjects()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(performer.position, InteractRadius, InteractableLayer);

        if(isInteractState == false)
        {
            if(detectedColliders.Length > 0)
                SetButtonState(true);
        }
        else
        {
            if(detectedColliders.Length <= 0)
                SetButtonState(false);
        }
    }

    private void SetButtonState(bool isInteractState)
    {
        this.isInteractState = isInteractState;
        button.image.sprite = isInteractState ? interactButtonSprite : attackButtonSprite;
        button.onClick.RemoveListener(isInteractState ? AttackEvent : InteractEvent);
        button.onClick.AddListener(isInteractState ? InteractEvent : AttackEvent);
    }

    private void AttackEvent() => OnAttackEvent?.Invoke();
    private void InteractEvent() => OnInteractEvent?.Invoke();

    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject != performer.gameObject && UnityEditor.Selection.activeObject != gameObject)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(performer.position, InteractRadius);
    }

    #endif
}
