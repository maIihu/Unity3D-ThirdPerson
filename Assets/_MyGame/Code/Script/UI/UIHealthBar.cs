using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private MonoBehaviour healthSourceRaw;
    
    private IHasHealth _healthSource;

    private void Awake()
    {
        _healthSource = healthSourceRaw as IHasHealth;
    }

    private void OnEnable()
    {
        _healthSource.OnHealthChanged += UpdateOnHealthUI;
    }

    private void OnDestroy()
    {
        _healthSource.OnHealthChanged -= UpdateOnHealthUI;
    }

    public void UpdateOnHealthUI(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount  = Mathf.Clamp01(currentHealth / maxHealth);
    }
}
