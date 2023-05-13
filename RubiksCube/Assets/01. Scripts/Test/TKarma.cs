using UnityEngine;

public class TKarma : MonoBehaviour
{
    private Color[] colors = {Color.red, Color.blue, Color.green};

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Karma karma = PoolManager.Instance.Pop("Karma") as Karma;
            karma.SetColor(colors[Random.Range(0, colors.Length)] * 3f);
            karma.transform.position = transform.position + Random.insideUnitSphere;
        }
    }
}
