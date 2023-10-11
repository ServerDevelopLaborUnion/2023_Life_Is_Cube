using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] List<PoolableMono> poolingList;
    [SerializeField] List<KarmaColorInfo> karmaColors;
    private Dictionary<BiomeFlags, Color> karmaColorDictionary;
    public Dictionary<BiomeFlags, Color> KarmaColorDictionary => karmaColorDictionary;

    private void Awake()
    {
        if(Instance != null) {
            Debug.LogWarning("multiple GameManger instance is running");
            return;
        }

        Application.targetFrameRate = 60;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PoolManager.Instance = new PoolManager();
        poolingList.ForEach(p => PoolManager.Instance.CreatePool(p, transform));

        CameraManager.Instance = gameObject.AddComponent<CameraManager>();
        TimeController.Instance = gameObject.AddComponent<TimeController>();
        StageManager.Instance = GetComponent<StageManager>();

        karmaColorDictionary = new Dictionary<BiomeFlags, Color>();
        foreach(KarmaColorInfo colorInfo in karmaColors)
        {
            if(karmaColorDictionary.ContainsKey(colorInfo.biome) == false)
                karmaColorDictionary.Add(colorInfo.biome, colorInfo.karmaColor);
        }
    }
}