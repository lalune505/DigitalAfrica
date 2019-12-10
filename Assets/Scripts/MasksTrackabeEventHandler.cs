using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksTrackabeEventHandler: DefaultTrackableEventHandler
{
    private MasksCanvasController _masksCanvasController;
    private Animator _maskAnimator;
    private GameController _gameController;
    private void Awake()
    {
        _masksCanvasController = FindObjectOfType<MasksCanvasController>();
    }

    public void SetAnimator(Animator animator)
    {
        _maskAnimator = animator;
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        if (_maskAnimator != null)
        {
            _maskAnimator.SetTrigger("Appear");
        }
        
        _masksCanvasController.EnableTargetPanel(false);

    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        _gameController = GetComponentInChildren<GameController>();
        if (_gameController != null)
        {
            _gameController.StopGame();
        }
        
        _masksCanvasController.EnableTargetPanel(true);

        var audioSources = GetComponentsInChildren <AudioSource>();

        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    #endregion // PROTECTED_METHODS
}
