using System;
using UnityEngine;

public class FireballOrbit : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;   
    [SerializeField] private float radius = 3f;
    [SerializeField] private float moveSpeed = 10f;
    
    private Transform _bossTransform;
    private float _angle;
    
    private bool _fireActive;
    private Vector3 _moveDirection;   

    public void Setup(float startingAngle, Transform centerTarget)
    {
        _angle = startingAngle;
        _bossTransform = centerTarget;
    }

    private void Update()
    {
        if (!_fireActive && _bossTransform)
        {
            _angle += rotateSpeed * Time.deltaTime;
            float rad = _angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;
            transform.position = _bossTransform.position + new Vector3(x, 0, z);
        }
        else
        {
            transform.position += _moveDirection * (moveSpeed * Time.deltaTime);
        }
    }

    public void ShootAt(Vector3 targetPos)
    {
        _fireActive = true;
        _bossTransform = null;

        _moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable) && attackable.CharacterType == CharacterType.Player)
        {
            Debug.Log("attack player");
            attackable.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}