using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject loadingPanel;

    private void Awake()
    {
        HideLoading();
    }

    public void ShowWarningPanel()
    {
        warningPanel.SetActive(true);
        HideLoading();
    }

    private void HideWarningPanel()
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
    

    public void ShowLoading()
    {
        loadingPanel.SetActive(true);
    }

    private void HideLoading()
    {
        loadingPanel.SetActive(false); 
    }

    public void SetAnimalsScene(bool set)
    {
        SceneLoader.instance.SetAnimalsButtonWasPressed(set);
    }
}
