using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float speed;

    private Vector3 _direction;
    private bool _isFlying;

    private float _damage;
    
    public void SetupBullet(Vector3 direction, float damage)
    {
        _direction = direction;
        _damage = damage;
        _isFlying = true;
    }

    private void Update()
    {
        if (_isFlying)
        {
            transform.position += _direction * (speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            target.TakeDamage(10f);
        }
        BulletObjectPool.Instance.ReturnBulletObject(gameObject);
        GameObject hitEffect = Instantiate(hitEffectPrefab, other.transform.position, Quaternion.identity);
        _isFlying = false;
        Destroy(hitEffect, 1f);
    }

    private void OnDisable()
    {
        _isFlying = false;
    }
}
