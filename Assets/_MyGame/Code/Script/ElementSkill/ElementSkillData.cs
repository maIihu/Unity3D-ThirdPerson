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
    public Sprite icon;
    public PlayerProjectile skillPrefab;
    public BaseEffect effect;
}
