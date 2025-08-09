
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteEffect")]
public class IgniteEffect : BaseEffect
{
    public float damagePerSecond;

    public override void Apply(IApplyEffect target)
    {
        target.ApplyIgnite(damagePerSecond, Duraction);
    }
}
