using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIPanelLevelUp : MonoBehaviour
{
    private const int UpgradeSlotCount = 3;

    [Header("Upgrade Item")]
    [SerializeField] private GameObject upgradeItemPrefab;
    [SerializeField] private Transform upgradeContainer;

    [Header("Skill UI")] [SerializeField] private UIItemSkill skillUI;
    [SerializeField] private Transform skillUIContainer;

    [Header("All Element Skills")]
    [SerializeField] private List<ElementSkillData> allSkills;

    [Header("Player")]
    [SerializeField] private PlayerCombat playerCombat;

    private List<UIUpgradeItem> _upgradeSlots;

    private void Awake()
    {
        _upgradeSlots = new List<UIUpgradeItem>();
        CreateUpgradeSlots();
    }

    private void CreateUpgradeSlots()
    {
        for (int i = 0; i < UpgradeSlotCount; i++)
        {
            GameObject item = Instantiate(upgradeItemPrefab, upgradeContainer);
            if (item.TryGetComponent(out UIUpgradeItem upgradeItem))
            {
                _upgradeSlots.Add(upgradeItem);
            }
        }
    }

    public void ShowUpgradeOptions()
    {
        var options = allSkills;

        for (int i = 0; i < _upgradeSlots.Count; i++)
        {
            if (i < options.Count)
            {
                _upgradeSlots[i].gameObject.SetActive(true);
                _upgradeSlots[i].Setup(options[i], OnUpgradeSelected);
            }
            else
            {
                _upgradeSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnUpgradeSelected(ElementSkillData selectedSkill)
    {
        //Debug.Log("Người chơi đã chọn: " + selectedSkill.name);
        // if (playerCombat.GetSkillOwner().Count <= 1)
        // {
        //     GameObject newSkillUI = Instantiate(skillUI.gameObject, skillUIContainer);
        //     newSkillUI.TryGetComponent(out UIItemSkill uiItemSkill);
        //     uiItemSkill.ChangeImageSkill(selectedSkill.icon);
        //     //newSkillUI.GetComponentInChildren<UIItemSkill>().ChangeImageSkill(selectedSkill.icon);
        // }
        allSkills.Remove(selectedSkill);
        playerCombat.AddProjectile(selectedSkill);
        gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }
}
