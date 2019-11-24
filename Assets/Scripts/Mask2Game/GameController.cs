using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private ScoreArea scoreArea;

    [SerializeField] private List <GameObject> throwableGameObjects;
    

    private int _currentGoNumber = 0;

    private float _resetTimer = 3f;

    private bool _isFinished = true;

    private List<GameObject> _currentRoundPrefabs;
    
    void Start()
    {

       StartGame();
        
    }
    
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
        player.DestroyGO();
        
        if (scoreArea.collided)
        {
            scoreArea.collided = false;
            if (_currentRoundPrefabs.Count > 1)
            {
               _currentRoundPrefabs.RemoveAt(_currentGoNumber);
            }
            else
            {
                _isFinished = true;
                return;
            }
        }
        
        player.ConfigCurrentBall(Instantiate(GetRandomGameObject()));
        _resetTimer = 3f;
        
        
    }
    private GameObject GetRandomGameObject()
    {
        _currentGoNumber = Random.Range(0, _currentRoundPrefabs.Count);
        return _currentRoundPrefabs[_currentGoNumber];
    }

    public void StartGame()
    {
        _currentRoundPrefabs = throwableGameObjects;

        _currentGoNumber = 0;
        
        player.gameObject.SetActive(true);

        StartNewRound();
        
        _isFinished = false;
    }

    public void StopGame()
    {
        _isFinished = true;

        player.DestroyGO();
        
        player.gameObject.SetActive(false);

        _currentGoNumber = 0;

    }

    private void OnDisable()
    {
        StopGame();
    }
    
    
}
