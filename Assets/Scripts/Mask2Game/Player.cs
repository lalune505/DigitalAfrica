using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _currentBall;
    private Rigidbody _currentBallRigidBody;
    private Camera  _camera;
    
    private float _ballDistanceZ = 0.75f;
    private float _ballDistanceY = -0.3f;
    private float _throwForce = 300f;

    private bool _holding;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_holding)
        {
            var cameraTransform = _camera.transform;
            var transformUp = cameraTransform.up;
            var transformForward = cameraTransform.forward; 
            
            _currentBall.transform.position = cameraTransform.position + transformForward * _ballDistanceZ +
                                             transformUp * _ballDistanceY;
            if (
         #if UNITY_EDITOR 
                Input.GetMouseButtonDown(0) 
         #elif UNITY_IOS || UNITY_ANDROID
               Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began
         #endif
                )
            {
                Ray ray = Camera.main.ScreenPointToRay(
                 #if UNITY_EDITOR
                    Input.mousePosition);
                    #elif UNITY_IOS || UNITY_ANDROID
                     Input.GetTouch(0).position);
                    #endif
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ball")))
                {
                    ThrowGameObject();
                }
            }
        }
    }

    public void ConfigCurrentBall(GameObject ball)
    {
        _holding = true;        
        _currentBall = ball;
        _currentBallRigidBody = _currentBall.AddComponent<Rigidbody>();

        _currentBallRigidBody.useGravity = false;
    }

    private void ThrowGameObject()
    {
        _holding = false;
        _currentBallRigidBody.useGravity = true;
        _currentBallRigidBody.AddForce(_camera.transform.forward * _throwForce);
    }

    public void StopGame()
    {
        if (_currentBall != null)
        {
            Destroy(_currentBall);
            _holding = false;
        }
    }

    public bool IsHoldingBall()
    {
        return _holding;
    }
}
