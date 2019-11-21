using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanelController : MonoBehaviour
{
    [SerializeField] private CameraPermissionChecker cameraPermissionChecker;
    private bool _isChecked = false;
    public void OnAnimFinishedEvent()
    {
        if (_isChecked)
        {
            return;
        }

        cameraPermissionChecker.VerifyPermission();
       
        _isChecked = true;
    }
}
