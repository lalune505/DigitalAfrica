using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class TakeScreenShot : MonoBehaviour
{
	public string screenShotName = "screenshot.png";
	[SerializeField]
	private Button b1,b2;

	private Texture2D _image;
	private Texture2D _lastImage;
	private string _mainPath;

	private string _mCapturePath;
	
	private const string ScreenShotText = "";
	private const string EmailSubject = "";

	[HideInInspector] 
	public bool inProgress = false;

	private Canvas _shareCanvas;

	private void Awake()
	{
		_shareCanvas = gameObject.GetComponent<Canvas>();
		_shareCanvas.enabled = false;
	}

	public void TakePhoto()
	{
		if (!inProgress)
		{
			StartCoroutine(TakeAndShowPhoto());
		}
	}

	public void SharePhoto()
	{
		ShareScreenShotWithText(ScreenShotText);
	}

	public void CloseShareCanvas()
	{
		_shareCanvas.enabled = false;
	}
	private IEnumerator TakeAndShowPhoto()
	{  
		yield return StartCoroutine(CaptureScreenImage());
		
		ShowScreenShot();
		_shareCanvas.enabled = true;
		inProgress = false;
	}
	private IEnumerator CaptureScreenImage()
	{
		inProgress = true;
		yield return new WaitForEndOfFrame();

		Texture2D img = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
		img.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
		img.Apply();
		_image = img;
		//NativeGallery.SaveImageToGallery(_image,"Re:Store AR","photo{}.png" );
		_lastImage = _image;

	}

	private void ShowScreenShot(){
		if (_lastImage != null) {
			Image shareCanvasImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
			var rect = new Rect(0,0,_lastImage.width, _lastImage.height);
			Sprite s = Sprite.Create(_lastImage,rect, new Vector2(0.5f, 0.5f));
			shareCanvasImage.sprite = s;
		}
		//ShareScreenShotWithText(ScreenShotText);
	}
	
	private void ShareScreenShotWithText(string text)
	{
		string screenShotPath = Application.persistentDataPath + "/" + screenShotName;
		if(File.Exists(screenShotPath)) File.Delete(screenShotPath);

		b1.gameObject.SetActive(false);
		b2.gameObject.SetActive(false);

		ScreenCapture.CaptureScreenshot(screenShotName);
		StartCoroutine(DelayedShare(screenShotPath, text));
	}

	private IEnumerator DelayedShare(string screenShotPath, string text)
	{
		while(!File.Exists(screenShotPath)) {
			yield return new WaitForSeconds(.05f);
		}

		new NativeShare().AddFile(screenShotPath).SetSubject(EmailSubject).SetText(text).Share();
		
		Invoke(nameof(DelayedUiVisibility),2f);
	}

	private void DelayedUiVisibility()
	{
		b1.gameObject.SetActive(true);
		b2.gameObject.SetActive(true);
	}
	private float width
	{
		get
		{
			return Screen.width;
		}
	}

	private float height
	{
		get
		{
			return Screen.height;
		}
	}
}

