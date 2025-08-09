using System;
using UnityEngine;

public class MeteorAOE : MonoBehaviour
{
    [SerializeField] private float radius;       
    [SerializeField] private float damage;       
    [SerializeField] private float delayBeforeImpact;
    [SerializeField] private float damageInterval = 1f;

    private Transform _player;
    private float _damageTimer;

    private void Start()
    {
        _player = GameManager.Instance.GetPlayerTransform();
        _damageTimer = 0f;
    }

    private void Update()
    {
        _damageTimer += Time.deltaTime;

        if (_damageTimer >= damageInterval)
        {
            Impact();
            _damageTimer = 0f;
        }
    }

    private void Impact()
    {
        float dist = Vector3.Distance(_player.position, transform.position);
        if (dist <= radius)
        {
            if (_player.TryGetComponent(out PlayerCombat playerCombat))
            {
                playerCombat.TakeDamage(damage);
            }
        }
    }
}