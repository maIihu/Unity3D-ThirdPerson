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
    public BossFireBallSkill FireBallSkill { private set; get; }
    public BossDeadState DeadState { private set; get; }

    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public GameObject fireBall;

    private void Start()
    {
        StateMachine = new BossStateMachine();
        IdleState = new BossIdleState(StateMachine, this);
        MoveState = new BossMoveState(StateMachine, this);
        ExplosionSkill = new BossExplosionSkill(StateMachine, this);
        FireBallSkill = new BossFireBallSkill(StateMachine, this);
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

    public void StartFireBallSkill()
    {
        int fireballCount = 5;
        float angleStep = 360f / fireballCount;
        float radius = 1.5f;

        for (int i = 0; i < fireballCount; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;

            GameObject fb = Instantiate(fireBall, spawnPos, Quaternion.identity);

            FireballOrbit orbit = fb.AddComponent<FireballOrbit>();
            orbit.center = this.transform;
            orbit.speed = 50f;
            orbit.radius = radius;
            orbit.startingAngle = angle; // <-- Đây là phần quan trọng!
        }
    }

    
    
}
