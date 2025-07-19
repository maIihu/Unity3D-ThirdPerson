using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPanelLevelUp panelLevelUpUI;

    private void Start()
    {
        panelLevelUpUI.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelLevelUpUI.ShowUpgradeOptions();
            panelLevelUpUI.gameObject.SetActive(true);
            GameManager.Instance.ChangeState(GameState.LevelUp);
        }
    }
}
