using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject splashPanel;
    [SerializeField] private CameraPermissionChecker cameraPermissionChecker;

    void Start()
    {
        splashPanel.SetActive(true);
       // splashPanel.GetComponent<SplashPanelController>().supportCanvasController = this;
    }
    public void ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        HideLoading();
    }

    public void HideWarningPanel()
    {
        warningPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        NativeSettings.GetSettingsURL_Native();
        #endif
        
        #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidRuntimePermissions.OpenSettings();
        #endif
        
        HideWarningPanel();
    }
    
    public void VerifyPermissionOnStart()
    {
        cameraPermissionChecker.VerifyPermission();
        splashPanel.SetActive(false);
        ShowLoading();
    }

    private void ShowLoading()
    {
        loadingPanel.SetActive(true);
    }

    private void HideLoading()
    {
        loadingPanel.SetActive(false); 
    }
    
    
}
