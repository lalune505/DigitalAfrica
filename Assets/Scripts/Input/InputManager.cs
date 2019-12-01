using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _mainCamera;
    private LayerMask _layersToHit;
    private const string ElementsLayerName = "GameMask";
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
        _inputRedirects.Add(LayerMask.NameToLayer(ElementsLayerName), _gameController);
    
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
            }
        }
    }
    private void RedirectInputOccur(RaycastHit hit)
    {
        _inputRedirects[hit.collider.gameObject.layer]?.HandleInputOccur(hit);
    }
}
