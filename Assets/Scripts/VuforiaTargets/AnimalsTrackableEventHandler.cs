
using UnityEngine;
using Vuforia;

public class AnimalsTrackableEventHandler : DefaultTrackableEventHandler
{
    private AnimalsCanvasController _canvasController;
    private TargetPrefabsContainer _targetPrefabsContainer;
    private void Awake()
    {
        _canvasController = FindObjectOfType<AnimalsCanvasController>();
        _targetPrefabsContainer = GetComponent<TargetPrefabsContainer>();
    }

    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        
        _canvasController.EnablePrefabSwitcherButtons(true);
        
        TargetContentManager.ActivateTargetPrefab();

        _canvasController.EnableTargetPanel(false);

    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        
        _canvasController.EnablePrefabSwitcherButtons(false);
        
        TargetContentManager.UpdateNameTextField("");

        _canvasController.EnableTargetPanel(true);
    }

    #endregion // PROTECTED_METHODS
}
