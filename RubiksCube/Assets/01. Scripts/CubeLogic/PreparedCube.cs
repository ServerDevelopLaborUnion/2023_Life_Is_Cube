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

    public bool TryGetPreparedCube(BiomeFlags biome, out MidBossCube preCube)
    {
        preCube = null;

        if(preparedCubeMap.ContainsKey(biome))
            return false;

        preCube = preparedCubeMap[biome];
        return true;            
    }

    public void RemoveCube(BiomeFlags biome)
    {
        if(preparedCubeMap.ContainsKey(biome))
            preparedCubeMap.Remove(biome);
    }
}
