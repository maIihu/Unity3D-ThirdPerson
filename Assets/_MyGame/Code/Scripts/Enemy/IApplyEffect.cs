
using UnityEngine;

public interface IApplyEffect
{
    public void ApplyIgnite(float damagePerSecond, float duration);
    public void ApplySlow(float duration);
    public void ApplyKnockback(Vector3 direction, float force);
    public void ApplyStun(float duration);
}