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
    public bool sceneReadyToActivate;

    private List<TrackableBehaviour> _trackableBehaviours;
    
    public static SceneLoader instance;
    private AsyncOperation _asyncOperation;

    private const string AnimalsARScenePath = "Assets/Scenes/AnimalsARScene.unity";
    private const string MasksARScenePath = "Assets/Scenes/MasksARScene.unity";
    private const string MenuScenePath = "Assets/Scenes/MenuScene.unity";
    public bool animalsButtonWasPressed;

    private void Awake()
    {

        if (instance == null) {
            instance = this;
        } else if (instance != this)
        {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    public void SetAnimalsButtonWasPressed(bool pressed)
    {
        animalsButtonWasPressed = pressed;
    }
    public void LoadARScene()
    {
        if (animalsButtonWasPressed)
        {
            LoadAnimalsScene();
        }
        else
        {
            LoadMasksScene();
        }
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
                sceneReadyToActivate = true;
                _asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        
        if (scenePath.Equals(MasksARScenePath))
        {
           InstantiateMasksPrefabsOnTrackables(FindObjectsOfType<TrackableBehaviour>().ToList().OrderBy(go =>
               go.name).ToList(),scenePrefabsSet);
        }
        else if (scenePath.Equals(AnimalsARScenePath))
        {
            InstantiateAnimalsPrefabsOnTrackables(FindObjectsOfType<TrackableBehaviour>().ToList().OrderBy(go =>
                go.name).ToList(), scenePrefabsSet);
        }
        
        sceneReadyToActivate = false;
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

    private void InstantiateMasksPrefabsOnTrackables(List<TrackableBehaviour> trackableBehaviours, ScenePrefabsSet set)
    {
        for (int i = 0; i < set.targets.Length; i++)
        {
           Instantiate(set.targets[i], trackableBehaviours[i].gameObject.transform, false);
           trackableBehaviours[i].GetComponent<MasksTrackabeEventHandler>().SetAnimator(set.targets[i].GetComponent<Animator>());
        }
        FindObjectOfType<InputManager>().Init();
    }
    private void InstantiateAnimalsPrefabsOnTrackables(List<TrackableBehaviour> trackableBehaviours, ScenePrefabsSet set)
    {
        for (int i = 0; i < set.targets.Length; i++)
        {
            Instantiate(set.targets[i], trackableBehaviours[i].gameObject.transform, false);
           // trackableBehaviours[i].GetComponent<MasksTrackabeEventHandler>().SetAnimator(set.targets[i].GetComponent<Animator>());
        }
        FindObjectOfType<InputManager>().Init();
    }
    
}
