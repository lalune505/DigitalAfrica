using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksTrackabeEventHandler: DefaultTrackableEventHandler
{
    private CanvasController _canvasController;
    private void Awake()
    {
        //_canvasController = FindObjectOfType<CanvasController>();
       
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();


    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
       
    }

    #endregion // PROTECTED_METHODS
}
