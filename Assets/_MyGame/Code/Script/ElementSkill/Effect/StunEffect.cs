
using UnityEngine;

[CreateAssetMenu(fileName = "StunEffect")]
public class StunEffect: BaseEffect
{
    public override void Apply(IApplyEffect target)
    {
        target.ApplyStun(Duraction);
    }
}