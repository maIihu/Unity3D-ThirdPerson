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
    
    [Header("Skill")]
    [SerializeField] private GameObject skillPrefab;
    
    private Vector3 _mouseWorldPos;
    private float _health;
    private float _maxHealth;
    private float _damage;
    
    private List<ElementSkillData> _skillOwnerList;
    private List<BaseEffect> _effectOwnerList;
    
    private Coroutine _damageEffectCoroutine;
    
    private void Start()
    {
        _skillOwnerList  = new List<ElementSkillData>();
        _effectOwnerList = new List<BaseEffect>();
        
        _maxHealth = _health = data.health;
        _damage = data.damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        damageEffect.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger(PlayerString.AttackTrigger);
                FireRaycast();
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
        
        var bullet = bulletObjectPool.GetBulletObject();
        bullet.transform.position = spawnBulletPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(bulletDir);
        
        bullet.TryGetComponent(out BulletProjectileBase bulletProjectile);
        bulletProjectile.SetupBullet(bulletDir, _damage, 20f, BulletOwner.Player, bulletObjectPool, 2f, _effectOwnerList);

    }
    
    #region IAttackable
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
        if(_damageEffectCoroutine == null)
            _damageEffectCoroutine = StartCoroutine(ShowDamageEffect());
    }
    
    public BulletOwner BulletOwner { get=>BulletOwner.Player; set{} }
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
    
    public void AddSkill(ElementSkillData newSkill)
    {
        for (int i = 0; i < _skillOwnerList.Count; i++)
        {
            if (_skillOwnerList[i].element == newSkill.element)
            {
                if ((int)newSkill.skillLevel > (int)_skillOwnerList[i].skillLevel)
                {
                    _skillOwnerList[i] = newSkill;
                }
                return;
            }
        }

        _skillOwnerList.Add(newSkill);
        _effectOwnerList.Add(newSkill.effect);
        Debug.Log("Owner " + newSkill.element);
        // switch (_skillOwnerList.Count)
        // {
        //     case 1:
        //         _skill1 = _skillOwnerList[0];
        //         break;
        //     case 2:
        //         _skill2 = _skillOwnerList[1];
        //         break;
        // }
    }

    public List<ElementSkillData> GetSkillOwner()
    {
        return  _skillOwnerList; 
    }
    
    #region Old System Skill
    
    // private ElementSkillData _skill1;
    // private ElementSkillData _skill2;
    // private ElementSkillData _ultimateSkill;
    // private void UseSkill()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         if (_skill1)
    //         {
    //             GameObject skill = Instantiate(_skill1.skillPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
    //             skill.TryGetComponent(out ElementSkillBase skillBase);
    //             skillBase.Setup(_skill1.moveSpeed, _skill1.timeLife, _skill1.damage);
    //         }
    //     }
    //     
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         if (_skill2)
    //         {
    //             GameObject skill = Instantiate(_skill2.skillPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
    //             skill.TryGetComponent(out ElementSkillBase skillBase);
    //             skillBase.Setup(_skill2.moveSpeed, _skill2.timeLife, _skill2.damage);
    //         }
    //     }
    // }

    #endregion
}
