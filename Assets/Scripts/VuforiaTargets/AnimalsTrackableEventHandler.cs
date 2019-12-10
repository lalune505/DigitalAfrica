
using System;
using UnityEngine;
using Vuforia;

public class AnimalsTrackableEventHandler : DefaultTrackableEventHandler
{
    private AnimalsCanvasController _canvasController;
    private AnimalsCanvasController _animalsCanvasController;
    private void Awake()
    {
        _canvasController = FindObjectOfType<AnimalsCanvasController>();
        _animalsCanvasController = FindObjectOfType<AnimalsCanvasController>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        _canvasController.EnableTargetPanel(false);
        
       _animalsCanvasController.EnableButton(true);

    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        
        SoundManager.instance.PauseAudioSource();

        _canvasController.EnableTargetPanel(true);
        
        _animalsCanvasController.EnableButton(false);
    }

    #endregion // PROTECTED_METHODS
    
}
