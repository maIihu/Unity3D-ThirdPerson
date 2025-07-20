using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float speed;

    private Vector3 _targetPos;
    private bool _isFlying;
    
    public void Launch(Vector3 targetPos)
    {
        _targetPos = targetPos;
        _isFlying = true;
    }

    private void Update()
    {
        if (_isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPos) < 0.1f)
            {
                BulletObjectPool.Instance.ReturnBulletObject(gameObject);
                GameObject hitEffect = Instantiate(hitEffectPrefab, _targetPos, Quaternion.LookRotation(Vector3.forward));
                _isFlying = false;
                Destroy(hitEffect, 1f);
            }
        }
    }

    private void OnDisable()
    {
        _isFlying = false;
    }
}
