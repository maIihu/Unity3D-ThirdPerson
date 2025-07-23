using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyObjectPool enemyObjectPool; 
    [SerializeField] private BulletObjectPool bulletObjectPool;
    [SerializeField] private float spawnTimer;
    
    private float _nextSpawnTime;
    
    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            _nextSpawnTime = spawnTimer + Time.time;
            GameObject enemy = enemyObjectPool.GetEnemyObject();
            enemy.transform.position = this.transform.position;
            enemy.TryGetComponent(out EnemyBase enemyBase);
            enemyBase.bulletObjectPool =  bulletObjectPool;
        }
    }
}
