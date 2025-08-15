using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerString
{
    public const string  AttackTrigger = "Attack";
}

public class PlayerCombat : MonoBehaviour, IAttackable, IHasHealth
{
    [Header("Fire Bullet")]
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private float attackRaycastDistance;
    [SerializeField] private Transform mid;
    
    [Header("Data")] [SerializeField] private PlayerData data;
    
    [Header("Animation")] [SerializeField] private Animator anim;
    
    [Header("Camera")] [SerializeField] private Transform fpsCamera;

    [Header("Effect")] 
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject damageEffect;
    
    private List<PlayerProjectile> _projectilesOwner;
    private PlayerProjectile _currentProjectile;
    private int _indexProjectile;
    
    private Vector3 _mouseWorldPos;
    private float _health;
    private float _maxHealth;

    private float _nextTimeAttack;
    
    private Coroutine _damageEffectCoroutine;
    
    private void Start()
    {
        _projectilesOwner = new List<PlayerProjectile>();
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        damageEffect.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentState == GameState.Playing)
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScroll > 0)
            {
                _indexProjectile++;
                if(_indexProjectile >= _projectilesOwner.Count) _indexProjectile = 0;
                _currentProjectile = _projectilesOwner[_indexProjectile];
            }
            else if (mouseScroll < 0)
            {
                _indexProjectile--;
                if (_indexProjectile < 0) _indexProjectile = _projectilesOwner.Count - 1;
                _currentProjectile = _projectilesOwner[_indexProjectile];
            }
            
            if (Input.GetMouseButtonDown(0) && Time.time >= _nextTimeAttack)
            {
                anim.SetTrigger(PlayerString.AttackTrigger);
                FireRaycast();
                _nextTimeAttack = Time.time + _currentProjectile.data.cooldown;
            }
        }
        
    }
    
    private void FireRaycast()
    {
        Vector3 shootTargetPoint;
        muzzleEffect.Play();
         if (Physics.Raycast(fpsCamera.position, fpsCamera.forward, out var hit, attackRaycastDistance))
         {
             shootTargetPoint = hit.point;
         }
         else
         {
             shootTargetPoint = fpsCamera.position + fpsCamera.forward * attackRaycastDistance;
         }

         Vector3 bulletDir = (shootTargetPoint - spawnBulletPoint.position).normalized;
        
        //var bullet = bulletObjectPool.GetBulletObject();
        var bullet = Instantiate(_currentProjectile);
        bullet.transform.position = spawnBulletPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(bulletDir);
        
        bullet.TryGetComponent(out PlayerProjectile bulletProjectile);
        bulletProjectile.SetupBullet(bulletDir, bulletObjectPool, CharacterType.Player);

    }
    
    #region IAttackable
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        if(_damageEffectCoroutine == null)
            _damageEffectCoroutine = StartCoroutine(ShowDamageEffect());
    }
    
    public CharacterType GetCharacterType => CharacterType.Player;
    #endregion
    
    private IEnumerator ShowDamageEffect()
    {
        damageEffect.SetActive(true);
        yield return new WaitForSeconds(.5f);
        damageEffect.SetActive(false);
        _damageEffectCoroutine = null;
    }
    
    #region IHasHealth
    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
    #endregion
    
    public void AddProjectile(ElementSkillData newSkill)
    {
        _projectilesOwner.Add(newSkill.skillPrefab);
        if (_projectilesOwner.Count > 1) return;
        _indexProjectile = 0;
        _currentProjectile = _projectilesOwner[_indexProjectile];
    }

}
