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
    public BossExplosionSkill ExplosionSkill { private set; get; }
    public BossDeadState DeadState { private set; get; }

    [SerializeField] public GameObject explosionEffect;

    private void Start()
    {
        StateMachine = new BossStateMachine();
        IdleState = new BossIdleState(StateMachine, this);
        MoveState = new BossMoveState(StateMachine, this);
        ExplosionSkill = new BossExplosionSkill(StateMachine, this);
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

    public void StartExplosionSkill()
    {
        GameObject explosion = Instantiate(explosionEffect,  transform.position, Quaternion.identity);
        Destroy(explosion, 5f);
    }
    
}
