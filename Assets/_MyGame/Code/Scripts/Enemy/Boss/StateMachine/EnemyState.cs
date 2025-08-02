using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyBase Enemy;
    protected EnemyStateMachine StateMachine;

    public EnemyState(EnemyStateMachine stateMachine, EnemyBase enemy)
    {
        StateMachine = stateMachine;
        Enemy = enemy;
    }

    public virtual void Enter() {}
    public virtual void Exit(){}
    public virtual void LogicUpdate(){}
    public virtual void PhysicsUpdate(){}
    
}
