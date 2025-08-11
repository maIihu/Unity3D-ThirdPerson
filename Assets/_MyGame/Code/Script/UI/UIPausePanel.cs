using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private Button enterArea;

    private void Awake()
    {
        enterArea.onClick.AddListener(EnterArea);
    }

    private void EnterArea()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }
}
