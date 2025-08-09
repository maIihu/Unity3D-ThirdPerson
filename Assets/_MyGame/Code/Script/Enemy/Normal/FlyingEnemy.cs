
using System;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    [SerializeField] private GameObject dragonBomb;

    private void Start()
    {
        this.Type = EnemyType.Flying;
    }

    private void Update()
    {
        ChaseToPlayerTarget();
        Attack();
    }

    protected override void ChaseToPlayerTarget()
    {
        Vector3 targetPosition = TargetPlayer.transform.position;
        targetPosition.y = this.transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
    }

    protected override void Attack()
    {
        if (DistanceToPlayerXZ() <= data.attackRange)
        {
            if (Time.time - LastAttackTime >= data.attackCooldown)
            {
                Instantiate(dragonBomb, spawnBulletPoint.position, Quaternion.identity);
                LastAttackTime = Time.time;
            }
        }
    }

    private float DistanceToPlayerXZ()
    {
        Vector3 playerPos = TargetPlayer.transform.position;
        Vector3 enemyPos =  this.transform.position;
        playerPos.y = enemyPos.y = 0;
        return Vector3.Distance(playerPos, enemyPos);
    }
}
