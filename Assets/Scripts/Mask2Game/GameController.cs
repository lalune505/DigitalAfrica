using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour, IUserInteractable
{
    [SerializeField] private Player player;

    [SerializeField] private ScoreArea scoreArea;

    [SerializeField] private List <GameObject> throwableGameObjects;
    

    private int _currentGoNumber = 0;

    private float _resetTimer = 3f;

    private bool _isFinished = true;
    
    
    void Update()
    {
        if (_isFinished) return;
        
        if (!player.IsHoldingBall())
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer <= 0f)
            {
                StartNewRound();
            }
        }

    }
    private void StartNewRound()
    {
        player.StopGame();
        
        if (scoreArea.collided)
        {
            scoreArea.collided = false;
         /*   if (_currentRoundPrefabs.Count > 1)
            {
               _currentRoundPrefabs.RemoveAt(_currentGoNumber);
            }
            else
            {
                _isFinished = true;
                return;
            }*/
        }
        
        player.ConfigCurrentBall(Instantiate(GetRandomGameObject()));
        _resetTimer = 3f;
        
        
    }
    private GameObject GetRandomGameObject()
    {
        _currentGoNumber = Random.Range(0, throwableGameObjects.Count);
        return throwableGameObjects[_currentGoNumber];
    }

    public void StartGame()
    {
        _currentGoNumber = 0;

        StartNewRound();
        
        _isFinished = false;
    }

    public void StopGame()
    {
        if (_isFinished) return;
        _isFinished = true;

        player.StopGame();

        _currentGoNumber = 0;

        Debug.Log("Game over");

    }

    public void HandleInputOccur(RaycastHit hit)
    {
        if (_isFinished)
        {
            StartGame();
        }
    }

}
