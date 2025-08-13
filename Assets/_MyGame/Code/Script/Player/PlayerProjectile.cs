using System;
using UnityEngine;

public class PlayerProjectile : ProjectileBase
{
    [SerializeField] protected GameObject hitEffectPrefab;

    private void Start()
    {
        CharacterOwnerType = CharacterType.Player;
    }

    private void Update()
    {
        if (_isFlying)
        {
            transform.position += _direction * (10 * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            if (CharacterOwnerType != target.GetCharacterType)
                target.TakeDamage(10);
            else return;
        }
        _bulletObjectPool.ReturnBulletObject(gameObject);
        _isFlying = false;
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, other.transform.position, Quaternion.identity);
            Destroy(hitEffect, 2f);
        }
    
        if (_lifeTimerCoroutine != null)
            StopCoroutine(_lifeTimerCoroutine);
    }
}
