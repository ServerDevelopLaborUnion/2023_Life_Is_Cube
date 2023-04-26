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

        PoolManager.Instance = new PoolManager();
        poolingList.ForEach(p => PoolManager.Instance.CreatePool(p, transform));

        TimeController.Instance = gameObject.AddComponent<TimeController>();

        UIManager.Instance = new UIManager();
    }
}
