using System;
using UnityEngine;

public class PlayerProjectile : ProjectileBase
{
    [SerializeField] private GameObject hitEffectPrefab;

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
        TriggerHitEffect(other.gameObject.transform.position);
    }

    protected void TriggerHitEffect(Vector3 pos)
    {
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
            Destroy(hitEffect, 2f);
        }
    }
}
