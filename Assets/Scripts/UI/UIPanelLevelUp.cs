
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelLevelUp : MonoBehaviour
{
    [SerializeField] private GameObject panelLevelUpUI;
    
    [SerializeField] private GameObject upgradeSimplePrefab;
    [SerializeField] private Transform upgradeContainer;
    
    [SerializeField] private List<ElementSkillData> elementSkillDataList;
    
    private List<UIUpgradeItem> _upgradeItemList;

    private void Start()
    {
        _upgradeItemList = new List<UIUpgradeItem>();
        panelLevelUpUI.SetActive(false);
        SpawnUpgradeSimple();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var item in _upgradeItemList)
            {
                item.Setup(elementSkillDataList[0], OnUpgradeChosen);
            }
            panelLevelUpUI.SetActive(true);
            GameManager.Instance.ChangeState(GameState.LevelUp);
        }
    }

    private void OnUpgradeChosen(ElementSkillData obj)
    {
        Debug.Log("Người chơi đã chọn: " + obj.name);
        panelLevelUpUI.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }

    private void SpawnUpgradeSimple()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject item = Instantiate(upgradeSimplePrefab, upgradeContainer);
            item.TryGetComponent(out UIUpgradeItem upgradeItem);
            _upgradeItemList.Add(upgradeItem);
        }
    }
}
