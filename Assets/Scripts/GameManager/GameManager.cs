using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing, GameOver, LevelUp, Pause
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance{get{return _instance;}}
    
    private GameState _gameState;

    private void Awake()
    {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        InitializeState();
    }

    private void InitializeState()
    {
        _gameState = GameState.Playing;
        EnterState();
    }

    public void ChangeState(GameState newState)
    {
        _gameState = newState;
        EnterState();
    }

    private void EnterState()
    {
        switch (_gameState)
        {
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.LevelUp:
                Time.timeScale = 0;
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
        }
    }
}
