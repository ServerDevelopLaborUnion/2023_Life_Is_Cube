using System.Collections;
using UnityEngine;

public class Karma : PoolableMono
{
    private MaterialPropertyBlock karmaMatPropBlock;
    private MaterialPropertyBlock borderMatPropBlock;
    private MeshRenderer karmaMeshRenderer;
    private MeshRenderer borderMeshRenderer;

    private int emissionColorHash = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        karmaMeshRenderer = GetComponent<MeshRenderer>();
        borderMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        karmaMatPropBlock = new MaterialPropertyBlock();
        karmaMeshRenderer.GetPropertyBlock(karmaMatPropBlock);

        borderMatPropBlock = new MaterialPropertyBlock();
        borderMeshRenderer.GetPropertyBlock(borderMatPropBlock);
    }

    public void SetColor(Color color)
    {
        karmaMatPropBlock.SetColor(emissionColorHash, color);
        borderMatPropBlock.SetColor(emissionColorHash, color);

        karmaMeshRenderer.SetPropertyBlock(karmaMatPropBlock);
        borderMeshRenderer.SetPropertyBlock(borderMatPropBlock);
    }

    public override void Reset()
    {

    }

    public void PopAnimation(float popDistance, float popSpeed = 10f) => StartCoroutine(PopCoroutine(popDistance, popSpeed));

    private IEnumerator PopCoroutine(float popDistance, float popSpeed = 10f)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position - Vector3.up * popDistance;
        Vector2 randFactor = RandomOnUnitCircle() * popDistance;
        endPos.x += randFactor.x;
        endPos.z += randFactor.y;

        float popTime = Mathf.Max(0.3f, Vector3.Distance(startPos, endPos)) / popSpeed;
        float v0 = (endPos.y - startPos.y) + -Physics.gravity.y;
        float theta = 0f;
        float timer = 0f;

        //endPos.y += v0;

        while(theta < 1f)
        {
            timer += Time.deltaTime;
            theta = timer / popTime;

            Vector3 pos = Vector3.Lerp(startPos, endPos, theta);
            pos.y = startPos.y + (v0 * theta) - (-Physics.gravity.y * theta * theta);
            transform.position = pos;

            yield return null;
        }
    }

    private Vector2 RandomOnUnitCircle()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
