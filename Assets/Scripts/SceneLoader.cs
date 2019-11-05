using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class SceneLoader : MonoBehaviour
{
    public ScenePrefabsSet scenePrefabsSetAnimals;
    public static SceneLoader instance;
    private AsyncOperation _asyncOperation;

    private const string AnimalsARScenePath = "Assets/Scenes/AnimalsARScene.unity";
    private const string MasksARScenePath = "";
    

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        } 
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadAnimalsScene()
    {
        StartCoroutine(LoadAsyncScene(AnimalsARScenePath));
    }

    public void LoadMasksScene()
    {
        StartCoroutine(LoadAsyncScene(MasksARScenePath));
    }

    private IEnumerator LoadAsyncScene(string scenePath)
    {
        yield return null;

        _asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        VuforiaRuntime.Instance.InitVuforia();

        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            if (_asyncOperation.progress >= 0.9f && (uint) VuforiaRuntime.Instance.InitializationState > 0U)
            {
                _asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}
