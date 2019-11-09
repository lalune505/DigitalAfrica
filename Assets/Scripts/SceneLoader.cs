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
        
        if (scenePath.Equals(MasksARScenePath))
        {
           InstantiatePrefabsOnTrackables(FindObjectsOfType<TrackableBehaviour>().ToList().OrderBy(go =>
               go.name).ToList(),scenePrefabsSet);
        }
        else if (scenePath.Equals(AnimalsARScenePath))
        {
            InstantiatePrefabsSetOnTrackable(FindObjectOfType<TrackableBehaviour>(), scenePrefabsSet);
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

    private void InstantiatePrefabsSetOnTrackable(TrackableBehaviour trackableBehaviour, ScenePrefabsSet set)
    {
        List<GameObject> targetPrefabs = new List<GameObject>();
        for (int i = 0; i < set.targets.Length; i++)
        {
            var go = Instantiate(set.targets[i], trackableBehaviour.gameObject.transform, true);
            targetPrefabs.Add(go);
        }
        trackableBehaviour.GetComponent<TargetPrefabsContainer>().SetTarget(targetPrefabs, 0);
        
        
    }
    
    private void InstantiatePrefabsOnTrackables(List<TrackableBehaviour> trackableBehaviours, ScenePrefabsSet set)
    {
        for (int i = 0; i < set.targets.Length; i++)
        {
           var go = Instantiate(set.targets[i], trackableBehaviours[i].gameObject.transform, true);
        }
        
    }

}
