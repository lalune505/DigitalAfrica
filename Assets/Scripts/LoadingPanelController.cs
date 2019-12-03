using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelController : MonoBehaviour
{
    public Image loadingIcon;
    [SerializeField] private CameraPermissionChecker cameraPermissionChecker;
   
    private void CheckPermission()
    {
        cameraPermissionChecker.VerifyPermission();
    }

    private void OnEnable()
    {
        CheckPermission();
    }

    private void Update()
    {
        if (loadingIcon)
        {
            if (!SceneLoader.instance.sceneReadyToActivate)
            {
                loadingIcon.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime * 2);
            }
            else
            {
                loadingIcon.enabled = false;
            }
        }

    }
}
