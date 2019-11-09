using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject switcherButtons;
    public void ShowNextPrefab()
    {
        TargetContentManager.ShowNext();
    }
    
    public void ShowPrevPrefab()
    {
        TargetContentManager.ShowPrev();
    }

    public void ActivatePrefabSwitcherButtons(bool activate)
    {
        switcherButtons.SetActive(activate);
    }
}
