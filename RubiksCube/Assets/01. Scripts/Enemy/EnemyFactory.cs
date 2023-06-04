using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] List<EnemySpawnDataSO> spawnDatas;
    [SerializeField] Vector3 minPos = new Vector3(-178.5f, 147.5f, 69.5f);
    [SerializeField] Vector3 maxPos = new Vector3(-73.5f, 147.5f, 174.5f);
    [SerializeField] float positionFactor = 2.5f;
    [SerializeField] float cellSize = 105f;
    // [SerializeField] float spawnDelay = 3f;

    private Dictionary<BiomeFlags, List<AIBrain>> enemyDictionary;
    private CubeAxis topAxis;

    // private BiomeFlags biome;
    // private int cellIdx = 0;
    // private float timer = 0f;
    // private bool spawning = false;

    private void Awake()
    {
        enemyDictionary = new Dictionary<BiomeFlags, List<AIBrain>>();

        foreach (EnemySpawnDataSO data in spawnDatas)
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

    // private void Update()
    // {
    //     if(spawning == false)
    //         return;

    //     timer += Time.deltaTime;

    //     if(timer > spawnDelay)
    //     {
    //         timer = 0f;
    //         SpawnEnemy();
    //     }
    // }

    #if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                for (int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 1000; j++)
                    {
                        float xFactor = i % 3 * cellSize;
                        float zFactor = i / 3 * cellSize;
                        float xPos = Random.Range(minPos.x + xFactor + positionFactor+ 5f, maxPos.x + xFactor - positionFactor- 5f);
                        float zPos = Random.Range(minPos.z - zFactor + positionFactor+ 5f, maxPos.z - zFactor - positionFactor- 5f);

                        Vector3 pos = new Vector3(xPos, 147, zPos);

                        Debug.DrawLine(pos + Vector3.up * 10f, pos + Vector3.down * 10f, Color.red, 10f);
                    }
                }
            }
        }
    }

    #endif

    public AIBrain SpawnEnemy(BiomeFlags biome, int cellIdx)
    {
        int randIdx = Random.Range(0, enemyDictionary[biome].Count);
        AIBrain enemy = PoolManager.Instance.Pop(enemyDictionary[biome][randIdx]) as AIBrain;

        float xFactor = cellIdx % 3 * cellSize;
        float zFactor = cellIdx / 3 * cellSize;
        float xPos = Random.Range(minPos.x + xFactor + positionFactor + 5f, maxPos.x + xFactor - positionFactor - 5f);
        float zPos = Random.Range(minPos.z - zFactor + positionFactor + 5f, maxPos.z - zFactor - positionFactor - 5f);
        enemy.transform.position = new Vector3(xPos, minPos.y, zPos);
        Debug.DrawLine(enemy.transform.position + Vector3.up * 1000f, enemy.transform.position + Vector3.down * 1000f, Color.red, 10f);

        enemy.Init();

        return enemy;
    }

    // public void StartSpawn(BiomeFlags biome, int cellIdx) 
    // {
    //     this.biome = biome;
    //     this.cellIdx = cellIdx;
    //     spawning = true;
    // }

    // public void StopSpawn()
    // {
    //     spawning = false;
    // }
}
