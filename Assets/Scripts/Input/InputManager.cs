﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MasksCanvasController masksCanvasController;
    public WeatherMaskManager weatherMaskManager;
    public MaskManager maskManager;
    public ColorMaskManager colorMaskManager;
    public MusicMaskManager musicMaskManager;
    public PredictionMaskManager predictionMaskManager;
    public AnimalsManager animalsManager;
    private Camera _mainCamera;
    private LayerMask _layersToHit;
    private const string GameMaskLayerName = "GameMask";
    private const string AnimationMasksLayerName = "Mask";
    private const string WeatherMaskLayerName = "WeatherMask";
    private const string ColorMaskLayerName = "ColorMask";
    private const string MusicMaskLayerName = "MusicMask";
    private const string PredictMaskLayerName = "PredictionMask";
    private const string AnimalsLayerName = "Animal";
    private GameController _gameController;
    
    private readonly Dictionary<int, IUserInteractable> _inputRedirects
        = new Dictionary<int, IUserInteractable>();

    public void Init()
    {
        _gameController = FindObjectOfType<GameController>();
        

        FillInputRedirects();
      
        _mainCamera = Camera.main;

        _layersToHit = LayerMask.GetMask(
            _inputRedirects
                .Keys
                .Select(LayerMask.LayerToName)
                .ToArray()
        );
    }

    void Update()
    {
#if UNITY_EDITOR
        RayCastWithClick();
#else 
        RayCastWithTouch();
#endif
    }

    private void FillInputRedirects()
    {
        _inputRedirects.Add(LayerMask.NameToLayer(GameMaskLayerName), _gameController);
        _inputRedirects.Add(LayerMask.NameToLayer(AnimationMasksLayerName), maskManager);
        _inputRedirects.Add(LayerMask.NameToLayer(ColorMaskLayerName), colorMaskManager);
        _inputRedirects.Add(LayerMask.NameToLayer(WeatherMaskLayerName), weatherMaskManager);
        _inputRedirects.Add(LayerMask.NameToLayer(MusicMaskLayerName), musicMaskManager);
        _inputRedirects.Add(LayerMask.NameToLayer(PredictMaskLayerName), predictionMaskManager);
        _inputRedirects.Add(LayerMask.NameToLayer(AnimalsLayerName), animalsManager);
    
    }

    private void RayCastWithTouch()
    { 
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                var ray = _mainCamera.ScreenPointToRay(Input.GetTouch(i).position);
            
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity,_layersToHit))
                {
                    RedirectInputOccur(hit);
                }
            }
        }
    }

    private void RayCastWithClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _layersToHit))
            {
                RedirectInputOccur(hit);
                if (masksCanvasController != null)
                    masksCanvasController.EnableTextPanel(false);
            }
        }
    }
    private void RedirectInputOccur(RaycastHit hit)
    {
        _inputRedirects[hit.collider.gameObject.layer]?.HandleInputOccur(hit);
    }
}
