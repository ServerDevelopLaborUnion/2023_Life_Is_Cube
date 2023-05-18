using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("multiple SceneLoader instance is running");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddSceneAsync(string sceneName, Action callback = null)
        => StartCoroutine(AsyncSceneLoader(sceneName, LoadSceneMode.Additive, callback));

    public void LoadSceneAsync(string sceneName, Action callback = null)
        => StartCoroutine(AsyncSceneLoader(sceneName, LoadSceneMode.Single, callback));

    public void UnloadSceneAsync(string sceneName, Action callback = null)
        => StartCoroutine(AsyncSceneUnloader(sceneName, callback));

    private IEnumerator AsyncSceneLoader(string sceneName, LoadSceneMode loadSceneMode, Action callback)
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        while (!asyncOper.isDone)
            yield return null;

        callback?.Invoke();
    }

    private IEnumerator AsyncSceneUnloader(string sceneName, Action callback)
    {
        AsyncOperation asyncOper = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncOper.isDone)
            yield return null;

        callback?.Invoke();
    }
}