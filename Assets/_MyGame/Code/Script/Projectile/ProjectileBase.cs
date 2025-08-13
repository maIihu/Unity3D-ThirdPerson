using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player, Enemy
}

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected ProjectileData data;

    protected Vector3 Direction;
    protected bool IsFlying;
    protected CharacterType CharacterOwnerType;
    
    private BulletObjectPool _bulletObjectPool;
    private Coroutine _lifeTimerCoroutine;
    
    private void OnEnable()
    {
        IsFlying = true;
        _lifeTimerCoroutine = StartCoroutine(BulletLifeTimer(data.lifeTime));
    }
    
    public void SetupBullet(Vector3 direction, BulletObjectPool bulletObjectPool, CharacterType characterOwnerType)
    {
        this.Direction = direction;
        _bulletObjectPool = bulletObjectPool;
        CharacterOwnerType = characterOwnerType;
    }

    private IEnumerator BulletLifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (!IsFlying) yield break;
        ReturnToPool();
    }
    
    protected void ReturnToPool()
    {
        IsFlying = false;
        _bulletObjectPool.ReturnBulletObject(gameObject);
        if (_lifeTimerCoroutine != null)
            StopCoroutine(_lifeTimerCoroutine);
    }


    
    private void OnDisable()
    {
        IsFlying = false;
    }

    protected abstract void ProjectileFly();
}
