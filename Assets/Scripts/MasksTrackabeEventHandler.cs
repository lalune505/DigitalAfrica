﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksTrackabeEventHandler: DefaultTrackableEventHandler
{
    public string maskText;
    private MasksCanvasController _masksCanvasController;
    private Animator _maskAnimator;
    private GameController _gameController;
    private bool _detected = false;
    private void Awake()
    {
        _masksCanvasController = FindObjectOfType<MasksCanvasController>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        
        
        if (!_detected)
        {
            if (_maskAnimator == null)
            {
                _maskAnimator = GetComponentInChildren<Animator>();
            }

            _maskAnimator.SetTrigger("Appear");

            _masksCanvasController.EnableTargetPanel(false);
            
            _masksCanvasController.EnableTextPanel(true);
            
            _masksCanvasController.UpdateTextPanel(maskText);
        }
        
        var audioSources = GetComponentsInChildren <AudioSource>();

        foreach (var audioSource in audioSources)
        {
            audioSource.mute = false;
        }

        _detected = true;


    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        
        _detected = false;

        _gameController = GetComponentInChildren<GameController>();
        
        if (_gameController != null)
        {
            _gameController.StopGame();
        }

        var audioSources = GetComponentsInChildren <AudioSource>();

        foreach (var audioSource in audioSources)
        {
            audioSource.mute = true;
        }
        // _masksCanvasController.EnableTextPanel(false);
    }

    #endregion // PROTECTED_METHODS
}
