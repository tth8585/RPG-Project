using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMono : MonoBehaviour { }
    public enum Scene {
        StartMenu,
        LoadingScene,
        Main,
        SelectScene,
    }
    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("loading gameobject");
            loadingGameObject.AddComponent<LoadingMono>().StartCoroutine(LoadSceneAsync(scene));
            //SceneManager.LoadSceneAsync(scene.ToString());
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            //return Mathf.Clamp01(loadingAsyncOperation.progress/0.9f);
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
