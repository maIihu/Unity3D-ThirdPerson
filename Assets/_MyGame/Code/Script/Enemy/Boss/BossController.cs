using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class BossController : MonoBehaviour, IHasHealth, IAttackable
{
    public BossState CurrentState { private set; get; }
    public BossStateMachine StateMachine { private set; get; }
    public BossIdleState IdleState { private set; get; }
    public BossMoveState MoveState { private set; get; }
    public BossExplosionSkill ExplosionSkill { private set; get; }
    public BossFireballSkill FireballSkill { private set; get; }
    public BossMeteorSkill MeteorSkill { private set; get; }
    public BossDeadState DeadState { private set; get; }

    [SerializeField] public GameObject explosionEffectPrefab;
    [SerializeField] public GameObject fireBallPrefab;
    [SerializeField] private GameObject meteorEffectPrefab;

    [SerializeField] private Transform skillSpawnPoint;
    
    private Transform _playerTarget;
    private List<FireballOrbit> _activeFireballOrbits;
    private bool _hasFireballAttackEnded;

    private float _maxHealth;
    private float _currenHealth;

    private void Start()
    {
        StateMachine = new BossStateMachine();
        IdleState = new BossIdleState(StateMachine, this);
        MoveState = new BossMoveState(StateMachine, this);
        ExplosionSkill = new BossExplosionSkill(StateMachine, this);
        FireballSkill = new BossFireballSkill(StateMachine, this);
        MeteorSkill = new BossMeteorSkill(StateMachine, this);
        DeadState = new BossDeadState(StateMachine, this);
        
        StateMachine.Initialize(IdleState);

        _playerTarget = GameManager.Instance.GetPlayerTransform();

        _maxHealth = 100;
        _currenHealth = _maxHealth;
    }
    
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void TriggerExplosionSkill()
    {
        GameObject explosion = Instantiate(explosionEffectPrefab, skillSpawnPoint.position, Quaternion.identity);
        Destroy(explosion, 5f);
    }

    public void TriggerFireballSkill()
    {
        _activeFireballOrbits = new List<FireballOrbit>();
        _hasFireballAttackEnded = false;
        int fireballCount = 5;
        float angleStep = 360f / fireballCount;

        for (int i = 0; i < fireballCount; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPos = skillSpawnPoint.position;

            GameObject fb = Instantiate(fireBallPrefab, spawnPos, Quaternion.identity);
            fb.TryGetComponent(out FireballOrbit orbit);
            orbit.Setup(angle, skillSpawnPoint);

            _activeFireballOrbits.Add(orbit);
        }
        StartCoroutine(FireballAttackSequence());
    }

    private IEnumerator FireballAttackSequence()
    {
        foreach (var orbit in _activeFireballOrbits)
        {
            yield return new WaitForSeconds(2f); 
            orbit.ShootAt(_playerTarget.position); 
        }
        _hasFireballAttackEnded = true;
    }

    public void TriggerMeteorSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0,  Random.Range(-10, 10)) + new Vector3(skillSpawnPoint.position.x, 0, skillSpawnPoint.position.z);
            GameObject meteor = Instantiate(meteorEffectPrefab, pos, Quaternion.identity);
            Destroy(meteor, 4f);
        }
    }

    public bool HasFireballAttackEnded()
    {
        return _hasFireballAttackEnded;
    }

    #region IHasHealth

    public float CurrentHealth => _currenHealth;
    public float MaxHealth => _maxHealth;
    public event Action<float, float> OnHealthChanged;

    #endregion
    
    #region IAttackable

    public void TakeDamage(float damage)
    {
        _currenHealth -= damage;
        OnHealthChanged?.Invoke(_currenHealth, _maxHealth);
    }

    public bool IsDead()
    {
        return _currenHealth <= 0;
    }

    public CharacterType CharacterType => CharacterType.Enemy;

    #endregion

}
