using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private LayerMask aimColliderLayerMask = new  LayerMask();
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private Transform spawnBulletPoint;
    
    [SerializeField] private Animator anim;
    private Vector3 _mouseWorldPos;
    
    public BulletOwner BulletOwner { get; set; }

    private void Start()
    {
        BulletOwner = BulletOwner.Player;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            
            Vector3 aimDir = (_mouseWorldPos - spawnBulletPoint.position).normalized;
            
            var bulletObject = bulletObjectPool.GetBulletObject();
            bulletObject.transform.position = spawnBulletPoint.position;
            bulletObject.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);
            
            if(bulletObject.TryGetComponent(out BulletProjectile bulletProjectile))
            {
                bulletProjectile.bulletOwner = BulletOwner.Player;
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
        return  Team.Player;
    }

    public void TakeDamage()
    {
        Debug.Log("PLAYER TAKE DAMAGE");
    }
}
