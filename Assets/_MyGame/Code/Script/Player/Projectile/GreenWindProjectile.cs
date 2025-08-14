
using UnityEngine;

public class GreenWindProjectile : PlayerProjectile
{
    private void Update()
    {
        if(IsFlying) ProjectileFly();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            if (CharacterOwnerType != target.GetCharacterType)
            {
                target.TakeDamage(data.damage);
            }
            else return;
        }
        ReturnToPool();
        TriggerHitEffect(other.transform.position);
    }
}
