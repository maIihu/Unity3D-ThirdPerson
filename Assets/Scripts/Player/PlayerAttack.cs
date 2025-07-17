using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerString
{
    public static string AttackTrigger = "Attack";
}

public class PlayerAttack : MonoBehaviour, IAttackable, IHasHealth
{
    [SerializeField] private LayerMask aimColliderLayerMask = new  LayerMask();
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private PlayerData data;
    
    [SerializeField] private Animator anim;
    
    private Vector3 _mouseWorldPos;
    private float _health;
    private float _maxHealth;
    
    public BulletOwner BulletOwner { get; set; }

    private void Start()
    {
        BulletOwner = BulletOwner.Player;
        _maxHealth = _health = data.health;
        OnHealthChanged?.Invoke(_health, _maxHealth);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(PlayerString.AttackTrigger);
            
            Vector3 aimDir = (_mouseWorldPos - spawnBulletPoint.position).normalized;
            
            var bulletObject = bulletObjectPool.GetBulletObject();
            bulletObject.transform.position = spawnBulletPoint.position;
            bulletObject.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);
            
            if(bulletObject.TryGetComponent(out BulletProjectile bulletProjectile))
            {
                bulletProjectile.bulletOwner = BulletOwner.Player;
                bulletProjectile.damage =  data.damage;
                bulletProjectile.Launch();
            }
        }
        
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
        {
            //debugTransform.position = hit.point;
            _mouseWorldPos = hit.point;
        }
        
    }

    public Team GetTeam()
    {
        return Team.Player;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public float CurrentHealth => _maxHealth;
    public float MaxHealth => _health;
    public event Action<float, float> OnHealthChanged;
}
