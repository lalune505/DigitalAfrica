using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITestTrackableEventHandler
{
       
    void OnTrackableStateChanged(
        TestTrackableBehaviour.Status previousStatus,
        TestTrackableBehaviour.Status newStatus);
}

