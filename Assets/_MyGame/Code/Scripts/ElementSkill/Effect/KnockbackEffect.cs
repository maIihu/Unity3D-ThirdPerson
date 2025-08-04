using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackEffect")]
public class KnockbackEffect : BaseEffect
{
    public float force;
    public override void Apply(IApplyEffect target)
    {
        target.ApplyKnockback(new Vector3(), force);
    }
}
