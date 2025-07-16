using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletOwner
{
    Enemy, Player
}

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float speed = 14f;
    private Rigidbody _bulletRigidBody;
    public BulletOwner bulletOwner;
    
    private void Awake()
    {
        _bulletRigidBody = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        _bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnDisable()
    {
        _bulletRigidBody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.TryGetComponent(out IAttackable attackable))
        {
            if ((bulletOwner == BulletOwner.Player && attackable.GetTeam() == Team.Enemy) ||
                (bulletOwner == BulletOwner.Enemy && attackable.GetTeam() == Team.Player))
            {
                Debug.Log("BULLET");
                attackable.TakeDamage();
            }
        }
        BulletObjectPool.Instance.ReturnBulletObject(gameObject);
    }
}
