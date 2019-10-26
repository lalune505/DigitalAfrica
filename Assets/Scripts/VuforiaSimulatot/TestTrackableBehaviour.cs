using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrackableBehaviour : MonoBehaviour
{
    protected TestTrackableBehaviour.Status mStatus;
    private List<ITestTrackableEventHandler> mTrackableEventHandlers = new List<ITestTrackableEventHandler>();
    protected string mTrackableName = "";
    public TestTrackableBehaviour.Status CurrentStatus
    {
        get
        {
            return this.mStatus;
        }
    }
    private void Awake()
    {
        mTrackableName = gameObject.name;
    }
    public enum Status
    {
        /// <summary>Pose for the trackable could not be delivered.</summary>
        NO_POSE,
        /// <summary>The tracking is limited.</summary>
        LIMITED,
        /// <summary>The trackable was detected.</summary>
        DETECTED,
        /// <summary>The trackable was tracked.</summary>
        TRACKED,
        /// <summary>The trackable was extended tracked.</summary>
        EXTENDED_TRACKED,
    }
    public void RegisterTrackableEventHandler(ITestTrackableEventHandler trackableEventHandler)
    {
        this.mTrackableEventHandlers.Add(trackableEventHandler);
        trackableEventHandler.OnTrackableStateChanged(TestTrackableBehaviour.Status.NO_POSE, this.mStatus);
    }
    public bool UnregisterTrackableEventHandler(ITestTrackableEventHandler trackableEventHandler)
    {
        return this.mTrackableEventHandlers.Remove(trackableEventHandler);
    }
    public string TrackableName
    {
        get
        {
            return this.mTrackableName;
        }
    }
    public virtual void OnTrackerUpdate(TestTrackableBehaviour.Status newStatus)
    {
        TestTrackableBehaviour.Status mStatus = this.mStatus;
        this.mStatus = newStatus;
        if (mStatus != newStatus)
        {
            foreach (ITestTrackableEventHandler trackableEventHandler in this.mTrackableEventHandlers)
                trackableEventHandler.OnTrackableStateChanged(mStatus, newStatus);
        }
    }
}
