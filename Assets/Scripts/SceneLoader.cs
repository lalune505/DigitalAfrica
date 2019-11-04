using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private AsyncOperation _asyncOperation;

    private bool IsReady = false;
    
    private const string AnimalsScenePath = "Assets/Scenes/AnimalsScene.unity";
    private const string MasksScenePath = "";
    
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
        SceneManager.LoadScene(AnimalsScenePath);
        StartCoroutine(LoadAsyncScene(AnimalsARScenePath));
    }

    public void LoadMasksScene()
    {
        StartCoroutine(LoadAsyncScene(MasksARScenePath));
    }
    
    private IEnumerator LoadAsyncScene(string scenePath)
    {
        yield return null;

        Scene currentScene = SceneManager.GetActiveScene();

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
         
        MoveGameObjectsToScene(SceneManager.GetSceneByPath(scenePath));

        SceneManager.UnloadSceneAsync(currentScene);

    }

    private void MoveGameObjectsToScene(Scene sceneToMove)
    {
        TestTargetsController testTargetsController = FindObjectOfType<TestTargetsController>();
        TargetsController targetsController = FindObjectOfType<TargetsController>();
        
        
        for (var i = 0; i < targetsController.targets.Count; i++)
        {
             GameObject go = testTargetsController.testTargets[i].transform.GetChild(0).gameObject;
             go.transform.parent = null;
             SceneManager.MoveGameObjectToScene(go, sceneToMove);
             
             go.transform.SetParent(targetsController.targets[i].gameObject.transform);
        }
        
    }
}
