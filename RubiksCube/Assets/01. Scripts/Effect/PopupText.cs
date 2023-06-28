using UnityEngine;
using TMPro;
using System.Collections;

public class PopupText : PoolableMono
{
    [SerializeField] int fontSize;
    [SerializeField] Color fontColor;
    private TextMeshPro tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshPro>();
    }

    public void Setting(int fontSize, Color fontColor)
    {
        this.fontSize = fontSize;
        this.fontColor = fontColor;
    }

    public void Pop(string text, Vector3 pos, float time = 1f, float yDelta = 2f)
    {
        tmpText.SetText(text);
        tmpText.color = fontColor;
        tmpText.fontSize = fontSize;
        transform.position = pos;

        StartCoroutine(DisplayCoroutine(time, yDelta));
    }

    private IEnumerator DisplayCoroutine(float time, float yDelta)
    {
        float currentTime = 0f;
        float percent = 0;
        Vector3 firstPos = transform.position;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;

            float nextY = yDelta * EaseInExpo(percent);
            float nextOpacity = Mathf.Lerp(1, 0, EaseInExpo(percent));

            transform.position = firstPos + new Vector3(0, nextY, 0);
            tmpText.alpha = nextOpacity;

            Vector3 camDir = (transform.position - DEFINE.MainCam.transform.position).normalized;

            transform.rotation = Quaternion.LookRotation(camDir);
            yield return null;
        }

        PoolManager.Instance.Push(this);
    }
    
    private float EaseInExpo(float t) {
        return (t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10));
    }

    public override void Reset()
    {

    }
}
