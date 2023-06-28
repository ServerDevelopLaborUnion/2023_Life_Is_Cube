using UnityEngine;

[System.Serializable]
public struct KarmaColorInfo
{
    public BiomeFlags biome;
    public Color karmaColor;
}

public class KarmaDropper : MonoBehaviour
{
    [SerializeField] BiomeFlags biome;
    [SerializeField, Range(0, 50)] int minKarmaRate = 0;
    [SerializeField, Range(0, 50)] int maxKarmaRate = 10;

    public void SpawnKarma()
    {
        if(maxKarmaRate < minKarmaRate)
            maxKarmaRate = minKarmaRate;

        int count = Random.Range(minKarmaRate, maxKarmaRate);
        for(int i = 0; i < count; i++)
        {
            Karma karma = PoolManager.Instance.Pop("Karma") as Karma;
            karma.SetColor(GameManager.Instance.KarmaColorDictionary[biome]);
            karma.transform.position = transform.position + Vector3.up * 4f;
            karma.PopAnimation(3f, 5f);
            karma.Absorb(2f);
        }
    }

    // public void Update()
    // {
    //     if(Input.GetButtonDown("Jump"))
    //         SpawnKarma();
    // }
}
