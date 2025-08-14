using System;
using UnityEngine;

public abstract class PlayerProjectile : ProjectileBase
{
    [SerializeField] private GameObject hitEffectPrefab;
    
    protected override void ProjectileFly()
    {
        transform.position += Direction * (data.speed * Time.deltaTime);
    }
    
    protected void TriggerHitEffect(Vector3 pos)
    {
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
            Destroy(hitEffect, 2f);
        }
    }
    
    protected abstract void OnTriggerEnter(Collider other);
}
