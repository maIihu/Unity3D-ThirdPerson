
using UnityEngine;

public class GroundEnemyProjectile : ProjectileBase
{
    private void Start()
    {
        CharacterOwnerType = CharacterType.Enemy;
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
    
        if (_lifeTimerCoroutine != null)
            StopCoroutine(_lifeTimerCoroutine);
    }
    
}
