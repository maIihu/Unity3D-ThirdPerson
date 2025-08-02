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
    
    private Coroutine _lifeTimerCoroutine;
    
    public void SetupBullet(Vector3 direction, float damage, float speed, BulletOwner bulletOwner, BulletObjectPool bulletObjectPool)
    {
        this._direction = direction;
        _damage = damage;
        _speed = speed;
        _isFlying = true;
        _bulletOwner = bulletOwner;
        _bulletObjectPool = bulletObjectPool;
        _lifeTimerCoroutine = StartCoroutine(BulletLifeTimer(3f));
    }

    private IEnumerator BulletLifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (_isFlying)
        {
            _isFlying = false;
            _bulletObjectPool.ReturnBulletObject(gameObject);
        }
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
        // if (other.GetComponent<BulletProjectileBase>() != null)
        //     return;
        //
        if (other.TryGetComponent(out IAttackable target))
        {
            if (target.BulletOwner != _bulletOwner)
                target.TakeDamage(_damage);
            else
                return;
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

    private void OnDisable()
    {
        _isFlying = false;
    }
}
