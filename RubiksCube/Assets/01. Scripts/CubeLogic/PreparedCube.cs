using System.Collections.Generic;
using UnityEngine;

public class PreparedCube : MonoBehaviour
{
    private Dictionary<BiomeFlags, MidBossCube> preparedCubeMap = null;

    private void Awake()
    {
        preparedCubeMap = new Dictionary<BiomeFlags, MidBossCube>();

        foreach(BiomeFlags biome in typeof(BiomeFlags).GetEnumValues())
        {
            if(preparedCubeMap.ContainsKey(biome))
                continue;

            if(transform.Find($"{biome.ToString()}Cube").TryGetComponent(out MidBossCube cube))
                preparedCubeMap.Add(biome, cube);
        }
    }

    public MidBossCube GetPreparedCube(BiomeFlags biome) => preparedCubeMap[biome];
}
