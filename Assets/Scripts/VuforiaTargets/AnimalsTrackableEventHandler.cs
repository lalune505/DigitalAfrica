
using UnityEngine;
using Vuforia;

public class AnimalsTrackableEventHandler : DefaultTrackableEventHandler
{
    private CanvasController _canvasController;
    private void Awake()
    {
        _canvasController = FindObjectOfType<CanvasController>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        _canvasController.ActivatePrefabSwitcherButtons(true);
       
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        _canvasController.ActivatePrefabSwitcherButtons(false);
    }

    #endregion // PROTECTED_METHODS
}
