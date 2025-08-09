
using UnityEngine;
using UnityEngine.UI;

public class UIItemSkill : MonoBehaviour
{
    [SerializeField] private Image imageSkill;

    public void ChangeImageSkill(Sprite img)
    {
        imageSkill.sprite = img;
    }
}
