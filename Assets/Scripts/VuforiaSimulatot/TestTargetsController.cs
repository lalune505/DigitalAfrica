using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTargetsController : MonoBehaviour
{
    public InputManager inputManger;
    public List<TestTrackableBehaviour> testTargets;
    public ScenePrefabsSet scenePrefabsSet;
    [SerializeField] private float offSet;
    private Camera _mainCamera;
    private Scene _currentScene;

    private const string MaskScenePath = "Assets/Scenes/MasksScene.unity";
    private const string AnimalsScenePath = "Assets/Scenes/AnimalsScene.unity";

    private AnimalsCanvasController _animalsCanvasController;
    private TargetPrefabsContainer targetPrefabsContainer;

    private void Start()
    {
        _mainCamera = Camera.main;
        
        _currentScene = SceneManager.GetActiveScene();

        _animalsCanvasController = FindObjectOfType<AnimalsCanvasController>();

        if (_currentScene.path.Equals(MaskScenePath))
        {
            InstantiatePrefabsOnTrackables(testTargets,scenePrefabsSet);
        } 
        else if (_currentScene.path.Equals(AnimalsScenePath))
        {
            Debug.Log("inst");
            InstantiatePrefabsSetOnTrackable(testTargets[0], scenePrefabsSet);
        }
        
        if (_animalsCanvasController != null)
        {
            _animalsCanvasController.EnablePrefabSwitcherButtons(false);
        }
    }

    public void OnMarkerFound(TestTrackableBehaviour trackableBehaviour)
    {
        UpdateTrackablesEditor(trackableBehaviour.TrackableName);
        
        trackableBehaviour.OnTrackerUpdate(TestTrackableBehaviour.Status.TRACKED);

        var trackableGo = trackableBehaviour.gameObject;
        
        _mainCamera.transform.position = trackableGo.transform.position + offSet * trackableGo.transform.up;
        
        _mainCamera.transform.eulerAngles = new Vector3(0, trackableGo.transform.eulerAngles.y, 0);
        
        if (_animalsCanvasController != null)
        {
            _animalsCanvasController.EnablePrefabSwitcherButtons(true);
        }

        if (targetPrefabsContainer != null)
        {
            TargetContentManager.ActivateTargetPrefab();
        }
        
        _animalsCanvasController.EnableTargetPanel(false);
        
        _animalsCanvasController.EnableButton(true);

    }

    public void HideAllMarkers()
    {
        foreach (TestTrackableBehaviour trackableBehaviour in testTargets)
        {
            trackableBehaviour.OnTrackerUpdate(TestTrackableBehaviour.Status.NO_POSE);
        }

        if (_animalsCanvasController != null)
        {
            _animalsCanvasController.EnablePrefabSwitcherButtons(false);
        }
        TargetContentManager.UpdateNameTextField("");
        
        
        _animalsCanvasController.EnableTargetPanel(true);
        
        _animalsCanvasController.EnableButton(false);
        
        SoundManager.instance.PauseAudioSource();
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
            var go = Instantiate(set.targets[i], trackableBehaviour.gameObject.transform, false);
            targetPrefabs.Add(go);
        }
        
        targetPrefabsContainer = trackableBehaviour.GetComponent<TargetPrefabsContainer>();
        
        targetPrefabsContainer.SetTarget(targetPrefabs, 0);
        
        TargetContentManager.SetCurrentTarget(targetPrefabsContainer.GetTarget(),
            targetPrefabsContainer.GetTransitionPrefab());

    }
    
    private void InstantiatePrefabsOnTrackables(List<TestTrackableBehaviour> trackableBehaviours, ScenePrefabsSet set)
    {
        for (int i = 0; i < set.targets.Length; i++)
        {
            Instantiate(set.targets[i], trackableBehaviours[i].gameObject.transform, false);
        }
        
        inputManger.Init();
        
    }
}
