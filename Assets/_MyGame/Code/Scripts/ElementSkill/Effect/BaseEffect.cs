
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    public string EffectName;
    public float Duraction;

    public abstract void Apply(EnemyBase enemyTarget);
}
