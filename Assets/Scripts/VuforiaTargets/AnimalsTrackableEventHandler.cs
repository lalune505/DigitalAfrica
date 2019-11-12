
using UnityEngine;
using Vuforia;

public class AnimalsTrackableEventHandler : DefaultTrackableEventHandler
{
    private CanvasController _canvasController;
    private TargetPrefabsContainer _targetPrefabsContainer;
    private void Awake()
    {
        _canvasController = FindObjectOfType<CanvasController>();
        _targetPrefabsContainer = GetComponent<TargetPrefabsContainer>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        _canvasController.ActivatePrefabSwitcherButtons(true);
        
        TargetContentManager.ActivateTargetPrefab();
        
        _canvasController.ShowOnTargetGOs(true);
        
        _canvasController.ShowTargetPanel(false);

    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        _canvasController.ActivatePrefabSwitcherButtons(false);
        
        TargetContentManager.UpdateNameTextField("");
        
        _canvasController.ShowOnTargetGOs(false);
        
        _canvasController.ShowTargetPanel(true);
    }

    #endregion // PROTECTED_METHODS
}
