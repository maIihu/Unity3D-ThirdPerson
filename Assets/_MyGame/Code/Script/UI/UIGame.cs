using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pausePanel;
    
    private GameManager _gameManager;
    private float _timer;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _timer = _gameManager.GameTimer;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (_gameManager.CurrentState == GameState.Playing)
        {
            _timer -= Time.deltaTime;
            SetTextTimer();
            if (Input.GetKeyDown(KeyCode.P)) ShowPausePanel();
        }
    }

    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        _gameManager.ChangeState(GameState.Pause);
    }

    private void SetTextTimer()
    {
        int minute = Mathf.FloorToInt(_timer / 60);
        int second = Mathf.FloorToInt(_timer % 60);
        timerText.text = $"{minute:00}:{second:00}";
    }
}
