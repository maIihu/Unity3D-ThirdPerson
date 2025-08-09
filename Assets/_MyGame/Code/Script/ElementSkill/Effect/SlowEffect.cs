
using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffect")]
public class SlowEffect : BaseEffect
{
    public override void Apply(IApplyEffect target)
    {
        target.ApplySlow(Duraction);
    }
}
