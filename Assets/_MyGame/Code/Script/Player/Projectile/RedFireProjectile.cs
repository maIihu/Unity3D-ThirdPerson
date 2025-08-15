
using System;
using UnityEngine;

public class RedFireProjectile : PlayerProjectile
{
    [SerializeField] private float explorerRadius;
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
               // target.TakeDamage(data.damage);
                Explorer(other.transform.position);
            }
            else return;
        }
        Destroy(gameObject);
        TriggerHitEffect(other.transform.position);
    }

    private void Explorer(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, explorerRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IAttackable target))
            {
                target.TakeDamage(data.damage);
            }
        }
    }
}
