using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player, Enemy
}

public class ProjectileBase : MonoBehaviour
{
    protected Vector3 _direction;
    protected bool _isFlying;
    protected CharacterType CharacterOwnerType;
    protected BulletObjectPool _bulletObjectPool;
    
    protected Coroutine _lifeTimerCoroutine;
    
    public void SetupBullet(Vector3 direction, BulletObjectPool bulletObjectPool, float lifeTimer)
    {
        this._direction = direction;
        _isFlying = true;
        _bulletObjectPool = bulletObjectPool;
        _lifeTimerCoroutine = StartCoroutine(BulletLifeTimer(lifeTimer));
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
    
    private void OnDisable()
    {
        _isFlying = false;
    }
}
