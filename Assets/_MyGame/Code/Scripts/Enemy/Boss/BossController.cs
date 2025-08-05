using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossState State { private set; get; }
    public BossStateMachine StateMachine { private set; get; }
    
    public BossIdleState IdleState { private set; get; }
    public BossMoveState MoveState { private set; get; }
    public BossAttackState AttackState { private set; get; }
    public BossDeadState DeadState { private set; get; }

    private void Start()
    {
        StateMachine = new BossStateMachine();
        IdleState = new BossIdleState(StateMachine, this);
        MoveState = new BossMoveState(StateMachine, this);
        AttackState = new BossAttackState(StateMachine, this);
        DeadState = new BossDeadState(StateMachine, this);
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
}
