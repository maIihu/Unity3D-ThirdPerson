using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class BossController : MonoBehaviour
{
    public BossState State { private set; get; }
    public BossStateMachine StateMachine { private set; get; }
    public BossIdleState IdleState { private set; get; }
    public BossMoveState MoveState { private set; get; }
    public BossExplosionSkill ExplosionSkill { private set; get; }
    public BossFireBallSkill FireBallSkill { private set; get; }
    public BossMeteorSkill MeteorSkill { private set; get; }
    public BossDeadState DeadState { private set; get; }

    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public GameObject fireBall;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject meteorEffect;

    private Transform _playerTarget;
    private List<FireballOrbit> _fireballOrbits;
    private bool _fireballAttack;

    private void Start()
    {
        StateMachine = new BossStateMachine();
        IdleState = new BossIdleState(StateMachine, this);
        MoveState = new BossMoveState(StateMachine, this);
        ExplosionSkill = new BossExplosionSkill(StateMachine, this);
        FireBallSkill = new BossFireBallSkill(StateMachine, this);
        MeteorSkill = new BossMeteorSkill(StateMachine, this);
        DeadState = new BossDeadState(StateMachine, this);
        
        StateMachine.Initialize(IdleState);

        _playerTarget = GameManager.Instance.GetPlayerTransform();
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
        GameObject explosion = Instantiate(explosionEffect, attackPoint.position, Quaternion.identity);
        Destroy(explosion, 5f);
    }

    public void StartFireBallSkill()
    {
        _fireballOrbits = new List<FireballOrbit>();
        _fireballAttack = false;
        int fireballCount = 5;
        float angleStep = 360f / fireballCount;

        for (int i = 0; i < fireballCount; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPos = attackPoint.position;

            GameObject fb = Instantiate(fireBall, spawnPos, Quaternion.identity);
            fb.TryGetComponent(out FireballOrbit orbit);
            orbit.Setup(angle, attackPoint);

            _fireballOrbits.Add(orbit);
        }
        StartCoroutine(FireballSequence());
    }

    private IEnumerator FireballSequence()
    {
        foreach (var orbit in _fireballOrbits)
        {
            yield return new WaitForSeconds(2f); 
            orbit.ShootAt(_playerTarget.position); 
        }

        _fireballAttack = true;
    }

    public void ActiveMeteorEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0,  Random.Range(-10, 10)) + new Vector3(attackPoint.position.x, 0, attackPoint.position.z);
            Instantiate(meteorEffect, pos, Quaternion.identity);
        }
    }

    public bool FireballAttackEnd()
    {
        return _fireballAttack;
    }

    
}
