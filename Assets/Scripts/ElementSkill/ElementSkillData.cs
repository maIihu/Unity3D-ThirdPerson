using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { Fire, Water, Wind, Earth, Lightning }

[CreateAssetMenu()]
public class ElementSkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public ElementType element;
    public int tier;
    public Sprite icon;
    public GameObject effectPrefab;
}
