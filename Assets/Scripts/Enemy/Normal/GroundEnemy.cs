
using System;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBase
{
    private NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        this.Type = EnemyType.Ground;
    }

    private void Update()
    {
        ChaseToPlayerTarget();
        Attack();
    }

    protected override void ChaseToPlayerTarget()
    {
        if (DistanceToPlayer() > data.attackRange)
        {
            Debug.DrawLine(TargetPlayer.position, TargetPlayer.position + Vector3.right, Color.red);
            _agent.SetDestination(TargetPlayer.position);
        }
        else
        {
            _agent.ResetPath();
        }
    }

    protected override void Attack()
    {
        if (DistanceToPlayer() <= data.attackRange)
        {
            if (Time.time - LastAttackTime >= data.attackCooldown)
            {
                LastAttackTime = Time.time;
                FireBullet();
            }
        }
    }
    
    private void FireBullet()
    {
        var bulletDir = transform.forward;
        var bullet = bulletObjectPool.GetBulletObject();
        bullet.transform.position = spawnBulletPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(bulletDir);
        
        bullet.TryGetComponent(out BulletProjectileBase bulletProjectile);
        bulletProjectile.SetupBullet(bulletDir, data.damage, 10f, BulletOwner.Enemy, bulletObjectPool);
    }
}
