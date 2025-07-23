using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IAttackable, IHasHealth
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;

    [SerializeField] private EnemyData data;

    [SerializeField] private GameObject expPrefab;
    
    private Transform _targetPlayer;
    private float _lastAttackTime;
    private float _health;
    private float _maxHealth;
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _targetPlayer = GameManager.Instance.GetPlayerTransform();
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    private void ChaseToPlayerTarget()
    {
        if (DistanceToPlayer() > data.attackRange)
        {
            Debug.DrawLine(_targetPlayer.position, _targetPlayer.position + Vector3.right, Color.red);
            _agent.SetDestination(_targetPlayer.position);
        }
        else
        {
            _agent.ResetPath();
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, _targetPlayer.transform.position);
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
        //Instantiate(bulletPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
        // var bullet = bulletObjectPool.GetBulletObject();
        // bullet.transform.position = spawnBulletPoint.position;
        // bullet.transform.rotation = Quaternion.identity;
        // bullet.TryGetComponent(out BulletProjectile bulletProjectile);
        //
        // Vector3 shootDir = (_targetPlayer.position - spawnBulletPoint.position).normalized;
        // bulletProjectile.SetupBullet(shootDir, 10f);
        
        var bulletDir = transform.forward;
        var bullet = bulletObjectPool.GetBulletObject();
        bullet.transform.position = spawnBulletPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(bulletDir);
        
        bullet.TryGetComponent(out BulletProjectileBase bulletProjectile);
        bulletProjectile.SetupBullet(bulletDir, data.damage, 10f, BulletOwner.Enemy, bulletObjectPool);
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        if(_health <= 0)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public BulletOwner BulletOwner { get=> BulletOwner.Enemy;
        set {  }
    }

    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
}
