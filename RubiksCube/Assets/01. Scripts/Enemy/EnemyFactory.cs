using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] List<EnemySpawnDataSO> spawnDatas;
    [SerializeField] Vector3 minPos = new Vector3(-178.5f, 147.5f, 69.5f);
    [SerializeField] Vector3 maxPos = new Vector3(-73.5f, 147.5f, 174.5f);
    [SerializeField] float positionFactor = 2.5f;
    [SerializeField] float cellSize = 105f;
    [SerializeField] float spawnDelay = 3f;

    private Dictionary<BiomeFlags, List<AIBrain>> enemyDictionary;
    private CubeAxis topAxis;
    private BiomeFlags biome;
    private int cellIdx = 0;
    private float timer = 0f;
    private bool spawning = false;

    private void Awake()
    {
        enemyDictionary = new Dictionary<BiomeFlags, List<AIBrain>>();

        foreach(EnemySpawnDataSO data in spawnDatas)
            enemyDictionary.Add(data.Biome, data.EnemyList);
    }

    // private void Start()
    // {
    //     cellIdx = 3;

    //     for(int i = 0; i < 10; i ++)
    //     {
    //         float xFactor = cellIdx % 3 * cellSize;
    //         float zFactor = cellIdx / 3 * cellSize;
    //         float xPos = Random.Range(minPos.x + xFactor + positionFactor, maxPos.x + xFactor - positionFactor);
    //         float zPos = Random.Range(minPos.z - zFactor + positionFactor, maxPos.z - zFactor - positionFactor);

    //         Debug.Log(new Vector3(xPos, minPos.y, zPos));
    //     }
    // }

    private void Update()
    {
        if(spawning == false)
            return;

        timer += Time.deltaTime;

        if(timer > spawnDelay)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    public void SpawnAtOnce(int cnt, BiomeFlags biome, int cellIdx)
    {
        this.biome = biome;
        this.cellIdx = cellIdx;

        for(int i = 0; i < cnt; i++)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randIdx = Random.Range(0, enemyDictionary[biome].Count);
        AIBrain enemy = PoolManager.Instance.Pop(enemyDictionary[biome][randIdx]) as AIBrain;

        float xFactor = cellIdx % 3 * cellSize;
        float zFactor = cellIdx / 3 * cellSize;
        float xPos = Random.Range(minPos.x + xFactor + positionFactor, maxPos.x + xFactor - positionFactor);
        float zPos = Random.Range(minPos.z - zFactor + positionFactor, maxPos.z - zFactor - positionFactor);
        enemy.transform.position = new Vector3(xPos, minPos.y, zPos);
    }

    public void StartSpawn(BiomeFlags biome, int cellIdx) 
    {
        this.biome = biome;
        this.cellIdx = cellIdx;
        spawning = true;
    }

    public void StopSpawn()
    {
        spawning = false;
    }
}
