using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject switcherButtons;

    [SerializeField] private TakeScreenShot takeScreenShot;

    [SerializeField] private GameObject[] gosToHide;

    [SerializeField] private GameObject targetPanel;
    
    private Canvas _mainCanvas;

    private void Awake()
    {
        _mainCanvas = gameObject.GetComponent<Canvas>();
    }

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

    public void TakePhoto()
    {
        StartCoroutine(PhotoCoroutine());
    }

    private IEnumerator PhotoCoroutine()
    {
        _mainCanvas.enabled = false;
        takeScreenShot.TakePhoto();

        while (takeScreenShot.inProgress)
        {
            yield return null;
        }

        _mainCanvas.enabled = true;
    }

    public void ShowOnTargetGOs(bool show)
    {
        foreach (var item in gosToHide)
        {
            item.SetActive(show);
        }
    }

    public void ShowTargetPanel(bool show)
    {
        targetPanel.SetActive(show);
    }
}
