using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI textDescription;
    
    [Header("Button")]
    [SerializeField] private Button selectButton;
    
    private ElementSkillData _skillData;
    private Action<ElementSkillData> _onClickCallback;

    public void Setup(ElementSkillData skillData, Action<ElementSkillData> onClickCallback)
    {
        _skillData = skillData;
        _onClickCallback = onClickCallback;
        
        textName.text = skillData.name;
        imageIcon.sprite = skillData.icon;
        textDescription.text = skillData.Description;
        
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClickCallback?.Invoke(_skillData);
    }
}
