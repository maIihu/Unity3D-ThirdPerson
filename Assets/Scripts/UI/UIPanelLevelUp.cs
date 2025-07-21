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

    [Header("Skill UI")] 
    [SerializeField] private UIItemSkill[] skillUI;

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
        var options = GetUpgradeOptions();

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
        if(playerCombat.GetSkillOwner().Count <= 1) skillUI[playerCombat.GetSkillOwner().Count].ChangeImageSkill(selectedSkill.icon);
        playerCombat.AddSkill(selectedSkill);
        gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }

    private List<ElementSkillData> GetUpgradeOptions()
    {
        List<ElementSkillData> playerSkills = playerCombat.GetSkillOwner();

        if (playerSkills.Count == 0)
        {
            return GetRandomBaseSkillsExcluding(null, UpgradeSlotCount);
        }

        if (playerSkills.Count == 1)
        {
            return GetRandomBaseSkillsExcluding(playerSkills[0].element, UpgradeSlotCount);
        }

        return GetRandomUpgradeFromCurrent(playerSkills);
    }


    private List<ElementSkillData> GetRandomBaseSkillsExcluding(ElementType? excludedElement, int count)
    {
        return allSkills
            .Where(skill => skill.skillLevel == SkillLevel.Base && (excludedElement == null || skill.element != excludedElement))
            .OrderBy(_ => Random.value)
            .Take(count)
            .ToList();
    }

    private List<ElementSkillData> GetRandomUpgradeFromCurrent(List<ElementSkillData> currentSkills)
    {
        var upgradeable = currentSkills
            .Where(skill => skill.nextLevelSkill)
            .Select(skill => skill.nextLevelSkill)
            .ToList();

        if (upgradeable.Count == 0) return new List<ElementSkillData>();

        return new List<ElementSkillData> { upgradeable[Random.Range(0, upgradeable.Count)] };
    }
}
