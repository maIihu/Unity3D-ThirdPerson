using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IAttackable, IHasHealth, IApplyEffect
{
    [SerializeField] protected Transform spawnBulletPoint;
    [SerializeField] protected EnemyData data;
    
    protected Transform TargetPlayer;
    protected float LastAttackTime;
    protected EnemyType Type;
    
    private float _health;
    private float _maxHealth;
    
    public BulletObjectPool bulletObjectPool;
    public int IndicatorID { get; private set; }
    private static int _nextID = 0;
    
    private void OnEnable()
    {
        TargetPlayer = GameManager.Instance.GetPlayerTransform();
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        IndicatorID = ++_nextID;
    }

    #region Base func

    protected float DistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, TargetPlayer.transform.position);
    }
    
    protected abstract void ChaseToPlayerTarget();
    protected abstract void Attack();

    #endregion
    
    #region IAttackable
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        if(_health <= 0)
        {
            //Instantiate(expPrefab, transform.position, Quaternion.identity);
            GameObject expObject = ExpObjectPool.Instance.GetExpObject();
            expObject.TryGetComponent(out ExpBall expBall);
            expBall.InitAt(this.transform.position);
            EnemyObjectPool.Instance.ReturnEnemyObject(this.Type, this.gameObject);
        }
    }

    public BulletOwner BulletOwner => BulletOwner.Enemy;
    #endregion
    
    #region IHasHealth
    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
    public event Action<Color> OnVisualChanged;
    #endregion
    
    public void ApplyIgnite(float damagePerSecond, float duration)
    {
        Debug.Log("AppyIgnite");
        ColorUtility.TryParseHtmlString("#FF6A00", out Color color);
        OnVisualChanged?.Invoke(color);
    }

    public void ApplySlow(float duration)
    {
        Debug.Log("ApplySlow");
        ColorUtility.TryParseHtmlString("#3FA7FF", out Color color);
        OnVisualChanged?.Invoke(color);
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        Debug.Log("ApplyKnockback");
        ColorUtility.TryParseHtmlString("#B8FFF1", out Color color);
        OnVisualChanged?.Invoke(color);
    }

    public void ApplyStun(float duration)
    {
        Debug.Log("ApplyStun");
        ColorUtility.TryParseHtmlString("#D4A017", out Color color);
        OnVisualChanged?.Invoke(color);
    }
}
