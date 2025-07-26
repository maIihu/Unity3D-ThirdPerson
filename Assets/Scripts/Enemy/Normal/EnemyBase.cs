using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IAttackable, IHasHealth
{
    [SerializeField] protected Transform spawnBulletPoint;
    [SerializeField] protected EnemyData data;
    [SerializeField] protected GameObject expPrefab;
    
    protected Transform TargetPlayer;
    protected float LastAttackTime;
    protected EnemyType Type;
    
    private float _health;
    private float _maxHealth;
    
    public BulletObjectPool bulletObjectPool;
    
    private void OnEnable()
    {
        TargetPlayer = GameManager.Instance.GetPlayerTransform();
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
    
    protected float DistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, TargetPlayer.transform.position);
    }
    
    protected abstract void ChaseToPlayerTarget();
    protected abstract void Attack();
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        OnVisualChanged?.Invoke(true);
        if(_health <= 0)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity);
            EnemyObjectPool.Instance.ReturnEnemyObject(this.Type, this.gameObject);
        }
    }

    public BulletOwner BulletOwner => BulletOwner.Enemy;
    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
    public event Action<bool> OnVisualChanged;
}
