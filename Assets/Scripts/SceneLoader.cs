using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private const string AnimalsScenePath = "";
    private const string MasksScenePath = "";
    
    private const string AnimalsARScenePath = "";
    private const string MasksARScenePath = "";
    

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        } else if (instance == this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadAnimalsScene()
    {
        
    }

    public void LoadMasksScene()
    {
        
    }
    
    private IEnumerator LoadAsyncScene(string scenePath)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
        MoveGameObjectsToScene();

        SceneManager.UnloadSceneAsync(currentScene);
        
    }

    private void MoveGameObjectsToScene()
    {
        foreach (var item in FindObjectOfType<TargetsController>().testTargetsBehaviours)
        {
           
        }

        
    }
}
