
using System;
using UnityEngine;

public class DragonBombProjectile : ProjectileBase
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private GameObject explodedEffect;

    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsFlying)
        {
            ProjectileFly();
        }
    }

    protected override void ProjectileFly()
    {
        _rb.MovePosition(transform.position + Vector3.down * (data.speed * Time.fixedDeltaTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
            Exploded();
    }

    private void Exploded()
    {
        //Debug.Log("Exploded");
        if (explodedEffect != null)
        {
            GameObject effect = Instantiate(explodedEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent<IAttackable>(out var target))
            {
                target.TakeDamage(data.damage);
            }
        }
        Destroy(gameObject);
    }
}
