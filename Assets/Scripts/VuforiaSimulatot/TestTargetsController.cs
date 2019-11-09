using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTargetsController : MonoBehaviour
{
    public List<TestTrackableBehaviour> testTargets;
    public ScenePrefabsSet scenePrefabsSet;
    [SerializeField] private float offSet;
    private Camera _mainCamera;
    private Scene _currentScene;

    private const string MaskScenePath = "Assets/Scenes/MasksScene.unity";
    private const string AnimalsScenePath = "Assets/Scenes/AnimalsScene.unity";

    private void Start()
    {
        _mainCamera = Camera.main;
        
        _currentScene = SceneManager.GetActiveScene();

        if (_currentScene.path.Equals(MaskScenePath))
        {
            InstantiatePrefabsOnTrackables(testTargets,scenePrefabsSet);
        } 
        else if (_currentScene.path.Equals(AnimalsScenePath))
        {
            Debug.Log("inst");
            InstantiatePrefabsSetOnTrackable(testTargets[0], scenePrefabsSet);
        }
        
    }

    public void OnMarkerFound(TestTrackableBehaviour trackableBehaviour)
    {
        UpdateTrackablesEditor(trackableBehaviour.TrackableName);
        
        trackableBehaviour.OnTrackerUpdate(TestTrackableBehaviour.Status.TRACKED);

        var trackableGo = trackableBehaviour.gameObject;
        
        _mainCamera.transform.position = trackableGo.transform.position + offSet * trackableGo.transform.up;
        
        _mainCamera.transform.eulerAngles = new Vector3(0, trackableGo.transform.eulerAngles.y, 0);
    }

    public void HideAllMarkers()
    {
        foreach (TestTrackableBehaviour trackableBehaviour in testTargets)
        {
            trackableBehaviour.OnTrackerUpdate(TestTrackableBehaviour.Status.NO_POSE);
        }
    }
    private void UpdateTrackablesEditor(string newTrackableName)
    {
        foreach (TestTrackableBehaviour trackableBehaviour in testTargets)
        {
            if (trackableBehaviour.TrackableName != newTrackableName)
                trackableBehaviour.OnTrackerUpdate(TestTrackableBehaviour.Status.NO_POSE);
        }
    }
    
    private void InstantiatePrefabsSetOnTrackable(TestTrackableBehaviour trackableBehaviour, ScenePrefabsSet set)
    {
        List<GameObject> targetPrefabs = new List<GameObject>();
        for (int i = 0; i < set.targets.Length; i++)
        {
            var go = Instantiate(set.targets[i], trackableBehaviour.gameObject.transform, true);
            targetPrefabs.Add(go);
        }
        trackableBehaviour.GetComponent<TargetPrefabsContainer>().SetTarget(targetPrefabs, 0);
    }
    
    private void InstantiatePrefabsOnTrackables(List<TestTrackableBehaviour> trackableBehaviours, ScenePrefabsSet set)
    {
        for (int i = 0; i < set.targets.Length; i++)
        {
            Instantiate(set.targets[i], trackableBehaviours[i].gameObject.transform, true);
        }
        
    }
}
