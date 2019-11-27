using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksTrackabeEventHandler: DefaultTrackableEventHandler
{
    private MainCanvasController _mainCanvasController;
    private Animator _maskAnimator;
    private void Awake()
    {
        //_canvasController = FindObjectOfType<CanvasController>();
       
    }

    public void SetAnimator(Animator animator)
    {
        _maskAnimator = animator;
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        _maskAnimator.SetTrigger("Appear");
        base.OnTrackingFound();


    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
       
    }

    #endregion // PROTECTED_METHODS
}
