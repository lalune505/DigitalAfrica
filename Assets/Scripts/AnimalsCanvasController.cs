using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject switcherButtons;

    [SerializeField] private GameObject targetPanel;
    
    public void ShowNextPrefab()
    {
        TargetContentManager.ShowNext();
    }
    
    public void ShowPrevPrefab()
    {
        TargetContentManager.ShowPrev();
    }    
    public void EnablePrefabSwitcherButtons(bool activate)
    {
        switcherButtons.SetActive(activate);
    }
    public void EnableTargetPanel(bool show)
    {
        targetPanel.SetActive(show);
    }
}
