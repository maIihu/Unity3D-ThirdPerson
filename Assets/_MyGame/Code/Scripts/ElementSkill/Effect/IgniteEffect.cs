
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteEffect")]
public class IgniteEffect : BaseEffect
{
    public float damagePerSecond;
    public override void Apply(EnemyBase enemyTarget)
    {
        enemyTarget.AppyIgnite(damagePerSecond, Duraction);
    }
}
