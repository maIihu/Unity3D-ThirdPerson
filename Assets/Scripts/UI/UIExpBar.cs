
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExpBar : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI expText;
    
    public void UpdateBarUI(float currentValue, float maxValue, float currentLevel)
    {
        expBar.fillAmount  = Mathf.Clamp01(currentValue / maxValue);
        expText.text = "Lv " + currentLevel;
    }
}
