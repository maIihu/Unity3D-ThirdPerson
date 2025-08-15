
using System;
using UnityEngine;

public class GroundEnemyProjectile : ProjectileBase
{
    private void Update()
    {
        if (IsFlying)
        {
            ProjectileFly();
        }
    }
    
    protected override void ProjectileFly()
    {
        transform.position += Direction * (data.speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            if (CharacterOwnerType != target.GetCharacterType)
                target.TakeDamage(data.damage);
            else return;
        }
        ReturnToPool();
    }
}
