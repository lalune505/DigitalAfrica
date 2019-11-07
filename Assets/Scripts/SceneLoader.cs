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
    private const string MenuScenePath = "Assets/Scenes/MenuScene.unity";
    

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
        StartCoroutine(LoadAsyncARScene(AnimalsARScenePath, animalsScenePrefabsSet));
    }

    public void LoadMasksScene()
    {
        StartCoroutine(LoadAsyncARScene(MasksARScenePath, masksScenePrefabsSet));
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadAsyncScene(MenuScenePath));
    }

    private IEnumerator LoadAsyncARScene(string scenePath, ScenePrefabsSet scenePrefabsSet)
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

        InstantiatePrefabsOnTrackables(scenePrefabsSet);
    }

    private void InstantiatePrefabsOnTrackables(ScenePrefabsSet set)
    {
        _trackableBehaviours = FindObjectsOfType<TrackableBehaviour>().ToList().OrderBy(go=>go.name).ToList();
        
        for (int i = 0; i < set.targets.Length; i++)
        {
            Transform trackableBehaviourTransform;
            
            if (_trackableBehaviours.Count == 1)
            {
                trackableBehaviourTransform = _trackableBehaviours[0].gameObject.transform;
            }
            else
            {
                trackableBehaviourTransform = _trackableBehaviours[i].gameObject.transform;
            }
            
            Instantiate(set.targets[i], trackableBehaviourTransform, true);
        }
    }

    private IEnumerator LoadAsyncScene(string scenePath)
    {
        yield return null;
        
        _asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Single);
        
        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            if (_asyncOperation.progress >= 0.9f)
            {
                _asyncOperation.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
   
}
