using System.Collections;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] Renderer meshRenderer;
    private MaterialPropertyBlock matPropBlock;
    private int blinkHash = Shader.PropertyToID("_Blink");

    [SerializeField] float blinkTime = 0.1f;
    [SerializeField] int blinkCount = 2;

    private void Awake()
    {
        matPropBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(matPropBlock);
    }

    public override void CreateFeedback()
    {
        StartCoroutine(BlinkCoroutine());
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();

        matPropBlock.SetInt(blinkHash, 0);
        meshRenderer.SetPropertyBlock(matPropBlock);
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount * 2; i++)
        {
            yield return new WaitForSeconds(blinkTime);

            matPropBlock.SetInt(blinkHash, ((i + 1) % 2));
            meshRenderer.SetPropertyBlock(matPropBlock);
        }
    }
}
