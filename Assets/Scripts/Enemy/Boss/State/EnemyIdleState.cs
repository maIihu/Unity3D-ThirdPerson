using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    //private float _patrolRange;
    public EnemyIdleState(EnemyStateMachine stateMachine, EnemyBase enemy) : base(stateMachine, enemy)
    {
    }
    
}
