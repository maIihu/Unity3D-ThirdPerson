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

    [SerializeField] private Transform playerTransform;
    public GameState CurrentState { get; private set; }

    public float GameTimer { get; private set; }

    private void Awake()
    {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        
        InitializeState();
        GameTimer = 60f;
    }

    private void Start()
    {
    }

    private void InitializeState()
    {
        CurrentState = GameState.Playing;
        EnterState();
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        EnterState();
    }

    private void EnterState()
    {
        switch (CurrentState)
        {
            case GameState.Pause:
            case GameState.LevelUp:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
