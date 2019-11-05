using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetsController : MonoBehaviour
{
    public List<TestTrackableBehaviour> testTargets;
    [SerializeField] private float offSet;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        
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
}
