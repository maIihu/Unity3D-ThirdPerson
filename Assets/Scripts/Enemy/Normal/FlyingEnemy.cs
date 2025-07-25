
using System;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    private void Update()
    {
        ChaseToPlayerTarget();
        Attack();
    }

    protected override void ChaseToPlayerTarget()
    {
        Vector3 targetPosition = _targetPlayer.transform.position;
        targetPosition.y = this.transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
    }

    protected override void Attack()
    {
        if (DistanceToPlayerXZ() <= data.attackRange)
        {
            Debug.Log("Attack");
        }
    }

    private float DistanceToPlayerXZ()
    {
        Vector3 playerPos = _targetPlayer.transform.position;
        Vector3 enemyPos =  this.transform.position;
        playerPos.y = enemyPos.y = 0;
        return Vector3.Distance(playerPos, enemyPos);
    }
}
