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
    
    public void PlayerLevelUp()
    {
        panelLevelUpUI.ShowUpgradeOptions();
        panelLevelUpUI.gameObject.SetActive(true);
        GameManager.Instance.ChangeState(GameState.LevelUp);
    }
}
