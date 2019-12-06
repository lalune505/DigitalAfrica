using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CameraPermissionChecker : MonoBehaviour {
	
	private SupportCanvasController _supportCanvasController;

	private void Awake()
	{
		_supportCanvasController = FindObjectOfType<SupportCanvasController>();
	}
	

	public void VerifyPermission()
	{
		
		#if UNITY_EDITOR
		SceneLoader.instance.LoadARScene();
		#endif
#if UNITY_IOS && !UNITY_EDITOR
        iOSCameraPermission.VerifyPermission(gameObject.name, "SampleCallback");
#endif
        
#if UNITY_ANDROID && !UNITY_EDITOR

       AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA");
       
       switch (result)
       {
	       case AndroidRuntimePermissions.Permission.Denied:
		       _supportCanvasController.ShowWarningPanel();
		       break;
	       case AndroidRuntimePermissions.Permission.Granted:
		       SceneLoader.instance.LoadARScene();
		       break;
	       case AndroidRuntimePermissions.Permission.ShouldAsk:
	       {
		       result = AndroidRuntimePermissions.RequestPermission("android.permission.CAMERA");
           
		       if (result == AndroidRuntimePermissions.Permission.Granted)
		       {
			        SceneLoader.instance.LoadARScene();
		       }
		       else
		       {
			       _supportCanvasController.ShowWarningPanel();
		       }

		       break;
	       }
       }
//       SaveManager.instance.SavePermissionRequest();

#endif
	}
	
	    
	private void SampleCallback(string permissionWasGranted)
    {       
        if (permissionWasGranted == "true" )
        {
	        SceneLoader.instance.LoadARScene();
        }
        else
        {
	        _supportCanvasController.ShowWarningPanel();
        }

		SaveManager.instance.SavePermissionRequest();
    }
	
	
}


