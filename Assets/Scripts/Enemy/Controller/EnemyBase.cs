using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IAttackable, IHasHealth
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;

    [SerializeField] private EnemyData data;
    
    private float _lastAttackTime;
    private float _health;
    private float _maxHealth;
    
    public BulletOwner BulletOwner { get; set; }
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        BulletOwner = BulletOwner.Enemy;
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    private void ChaseToPlayerTarget()
    {
        if (DistanceToPlayer() > data.attackRange)
        {
            Debug.DrawLine(targetPlayer.position, targetPlayer.position + Vector3.right, Color.red);
            _agent.SetDestination(targetPlayer.position);
        }
        else
        {
            _agent.ResetPath();
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, targetPlayer.transform.position);
    }
    
    private void Update()
    {
        ChaseToPlayerTarget();
        Attack();
    }
   
    private void Attack()
    {
        if (DistanceToPlayer() <= data.attackRange)
        {
            if (Time.time - _lastAttackTime >= data.attackCooldown)
            {
                _lastAttackTime = Time.time;
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        Vector3 directionToPlayer = (targetPlayer.transform.position - transform.position).normalized;
        
        var bulletObject = bulletObjectPool.GetBulletObject();
        bulletObject.transform.position = spawnBulletPoint.position;
        bulletObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);
            
        if(bulletObject.TryGetComponent(out BulletProjectile bulletProjectile))
        {
            bulletProjectile.bulletOwner = BulletOwner.Enemy;
            bulletProjectile.damage =  data.damage;
            bulletProjectile.Launch();
        }
    }

    public Team GetTeam()
    {
        return Team.Enemy;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
}
