using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { Fire, Water, Wind, Earth, Lightning }
public enum SkillLevel {Base, Upgrade1, Upgrade2}

[CreateAssetMenu()]
public class ElementSkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public ElementType element;
    public SkillLevel skillLevel;
    public Sprite icon;
    public GameObject effectPrefab;

    public ElementSkillData nextLevelSkill;
}
