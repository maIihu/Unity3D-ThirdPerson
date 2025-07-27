using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBomb : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject explodedEffect;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + Vector3.down * (fallSpeed * Time.fixedDeltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        Exploded();
    }

    private void Exploded()
    {
        Debug.Log("Exploded");
        if (explodedEffect != null)
        {
            GameObject effect = Instantiate(explodedEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayer);

        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent<IAttackable>(out var target))
            {
                target.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
