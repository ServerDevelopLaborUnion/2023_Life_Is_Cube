using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] List<PoolableMono> poolingList;

    private void Awake()
    {
        if(Instance != null) {
            Debug.LogWarning("multiple GameManger instance is running");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //PoolManager.Instance = new PoolManager();
        //poolingList.ForEach(p => PoolManager.Instance.CreatePool(p, transform));

        //CameraManager.Instance = new CameraManager();
        //UIManager.Instance = new UIManager();

        //TimeController.Instance = gameObject.AddComponent<TimeController>();
        SceneLoader.Instance = gameObject.AddComponent<SceneLoader>();
        //StageManager.Instance = GetComponent<StageManager>();
    }
}