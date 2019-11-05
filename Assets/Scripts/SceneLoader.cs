using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class SceneLoader : MonoBehaviour
{
    public ScenePrefabsSet animalsScenePrefabsSet;
    public ScenePrefabsSet masksScenePrefabsSet;

    private List<TrackableBehaviour> _trackableBehaviours;
    
    public static SceneLoader instance;
    private AsyncOperation _asyncOperation;

    private const string AnimalsARScenePath = "Assets/Scenes/AnimalsARScene.unity";
    private const string MasksARScenePath = "Assets/Scenes/MasksARScene.unity";
    

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
        StartCoroutine(LoadAsyncScene(AnimalsARScenePath, animalsScenePrefabsSet));
    }

    public void LoadMasksScene()
    {
        StartCoroutine(LoadAsyncScene(MasksARScenePath, masksScenePrefabsSet));
    }

    private IEnumerator LoadAsyncScene(string scenePath, ScenePrefabsSet scenePrefabsSet)
    {
        yield return null;

        _asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Single);

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

        _trackableBehaviours = FindObjectsOfType<TrackableBehaviour>().ToList().OrderBy(go=>go.name).ToList();
        
        for (int i = 0; i < scenePrefabsSet.targets.Length; i++)
        {
            Instantiate(scenePrefabsSet.targets[i], _trackableBehaviours[i].gameObject.transform, true);
        }

    }
}
