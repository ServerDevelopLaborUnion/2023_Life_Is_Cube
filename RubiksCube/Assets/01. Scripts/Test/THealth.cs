using UnityEngine;

public class THealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHP = 100f;
    private float currentHP = 0f;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHP = maxHP;
    }

    public void OnDamage(float damage, Vector3 point, Vector3 normal)
    {
        currentHP -= damage;
        Debug.Log($"{damage}만큼의 데미지 받음! 남은 체력 : {currentHP}");

        //노말벡터 테스트
        Debug.DrawLine(transform.position, transform.position + normal * 3f, Color.red, 1f);
        rb.AddForce(-normal * 3f, ForceMode.Impulse);

        if(currentHP <= 0f)
            Debug.Log("쥬금");
    }
}
