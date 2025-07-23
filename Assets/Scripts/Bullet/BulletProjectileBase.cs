using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletOwner
{
    Player, Enemy
}

public class BulletProjectileBase : MonoBehaviour
{
    [SerializeField] protected GameObject hitEffectPrefab;
    
    private Vector3 _direction;
    private bool _isFlying;
    private float _damage;
    private float _speed;
    private BulletOwner _bulletOwner;
    private BulletObjectPool _bulletObjectPool;
    
    public void SetupBullet(Vector3 direction, float damage, float speed, BulletOwner bulletOwner, BulletObjectPool bulletObjectPool)
    {
        this._direction = direction;
        _damage = damage;
        _speed = speed;
        _isFlying = true;
        _bulletOwner = bulletOwner;
        _bulletObjectPool = bulletObjectPool;
    }
    
    private void Update()
    {
        if (_isFlying)
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target) && target.BulletOwner != _bulletOwner)
        {
            target.TakeDamage(_damage);
        }
        _bulletObjectPool.ReturnBulletObject(gameObject);
        _isFlying = false;
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, other.transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f);
        }
        
    }

    private void OnDisable()
    {
        _isFlying = false;
    }
}
