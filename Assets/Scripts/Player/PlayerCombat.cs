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
    [Header("Fire")]
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private float attackRaycastDistance;
    
    [Header("Data")]
    [SerializeField] private PlayerData data;
    
    [Header("Animation")]
    [SerializeField] private Animator anim;
    
    [Header("Camera")]
    [SerializeField] private Transform fpsCamera;
    
    private Vector3 _mouseWorldPos;
    private float _health;
    private float _maxHealth;
    private float _damage;
    
    private List<ElementSkillData> _skillOwnerList;
    
    private void Start()
    {
        _skillOwnerList  = new List<ElementSkillData>();
        _maxHealth = _health = data.health;
        _damage = data.damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
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
        if (Physics.Raycast(fpsCamera.position, fpsCamera.forward, out var hit, attackRaycastDistance))
        {
            //Debug.Log(hit.collider.gameObject.name);
            // Debug.DrawRay(spawnBulletPoint.position, fpsCamera.forward * hit.distance, Color.red, 10);
            shootTargetPoint = hit.point;
            if (hit.collider.TryGetComponent(out IAttackable attackable))
            {
                attackable.TakeDamage(_damage);
            }
        }
        else
        {
            shootTargetPoint = fpsCamera.position + fpsCamera.forward * attackRaycastDistance;
        }
        Vector3 bulletDir = (shootTargetPoint  - spawnBulletPoint.position).normalized;
        
        var bullet = bulletObjectPool.GetBulletObject();
        bullet.transform.position = spawnBulletPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(bulletDir);
        
        bullet.TryGetComponent(out BulletProjectile bulletProjectile);
        bulletProjectile.Launch(shootTargetPoint);
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;

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
    }


    public List<ElementSkillData> GetSkillOwner()
    {
        return  _skillOwnerList; 
    }

}
