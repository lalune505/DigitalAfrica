using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasController : MonoBehaviour
{
    [SerializeField] private TakeScreenShot takeScreenShot;
    [SerializeField] private GameObject flashLight;

    private Canvas _mainCanvas;

    private void Awake()
    {
        _mainCanvas = gameObject.GetComponent<Canvas>();
        flashLight.SetActive(false);
    }

    public void TakePhoto()
    {
        StartCoroutine(PhotoCoroutine());
    }

    private IEnumerator PhotoCoroutine()
    {
        flashLight.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        flashLight.SetActive(false);
        
        _mainCanvas.enabled = false;
        takeScreenShot.TakePhoto();

        while (takeScreenShot.inProgress)
        {
            yield return null;
        }
        _mainCanvas.enabled = true;
    }
}
