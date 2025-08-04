using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { Fire, Water, Wind, Earth }
public enum SkillLevel {Base, Upgrade1, Upgrade2}

[CreateAssetMenu()]
public class ElementSkillData : ScriptableObject
{
    public string SkillName;
    public string Description;
    public ElementType element;
    public SkillLevel skillLevel;
    public Sprite icon;
    public GameObject skillPrefab;
    public float amount;
    public float timeLife;
    public float countDownTimer;
    public float moveSpeed;
    public float damage;
    public BaseEffect effect;

    public ElementSkillData nextLevelSkill;
}
