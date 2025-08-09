using System;
using UnityEngine;

public class ElementSkillBase : MonoBehaviour
{
    private float _moveSpeed;
    private float _lifeTime;
    private float _damage;
    private bool _isFiring;
    
    public void Setup(float speed, float lifeTime, float damage)
    {
        _moveSpeed = speed;
        _lifeTime = lifeTime;
        _damage = damage;
        _isFiring = true;
    }

    private void Update()
    {
        if(_isFiring)
        {
            transform.position += transform.forward * (_moveSpeed * Time.deltaTime);
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            target.TakeDamage(_damage);
        }
    }
}