
using System;
using UnityEngine;
using Vuforia;

public class AnimalsTrackableEventHandler : DefaultTrackableEventHandler
{
    private AnimalsCanvasController _canvasController;
    private AnimalsCanvasController _animalsCanvasController;
    private bool _detected;
    private void Awake()
    {
        _canvasController = FindObjectOfType<AnimalsCanvasController>();
        _animalsCanvasController = FindObjectOfType<AnimalsCanvasController>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        if (!_detected)
        {
            _canvasController.EnableTargetPanel(false);

            _animalsCanvasController.EnableButton(true);
        }

        _detected = true;
        
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        _detected = false;
        
        SoundManager.instance.PauseAudioSource();

        _canvasController.EnableTargetPanel(true);
        
        _animalsCanvasController.EnableButton(false);
    }

    #endregion // PROTECTED_METHODS
    
}
