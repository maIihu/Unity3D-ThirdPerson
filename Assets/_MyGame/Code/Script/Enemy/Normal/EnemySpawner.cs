using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Normal Enemy")]
    [SerializeField] private EnemyObjectPool enemyObjectPool; 
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private float spawnTimer;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float nextSpawnTime;
    
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = spawnTimer + Time.time;
            GameObject enemy = enemyObjectPool.GetEnemyObject(enemyType);
            enemy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            enemy.TryGetComponent(out EnemyBase enemyBase);
            enemyBase.bulletObjectPool =  bulletObjectPool;
        }
    }
}
