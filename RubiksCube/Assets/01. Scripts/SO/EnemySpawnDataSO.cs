using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySpawnData")]
public class EnemySpawnDataSO : ScriptableObject
{
    public BiomeFlags Biome;
    public List<AIBrain> EnemyList;
}
