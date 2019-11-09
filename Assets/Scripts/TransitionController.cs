using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public void HideTransitionCubeEvent()
    {
        TargetContentManager.HidePrefabAnimationEvent();
    }

    public void SwapPrefabs()
    {
        TargetContentManager.SwapPrefabsAnimationEvent();
    }
}
