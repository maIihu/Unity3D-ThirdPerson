using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDerector : MonoBehaviour
{
    [Header("Detection Settings")] 
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private LayerMask enemyLayer;

    private List<Transform> _detectedEnemyList;
    private Transform _closetEnemy;

    private void Start()
    {
        _detectedEnemyList = new List<Transform>();
        StartCoroutine(EnemyScanRoutine());
    }

    private void Update()
    {
        
        
    }

    private IEnumerator EnemyScanRoutine()
    {
        while (true)
        {
            ScanForEnemies();
            yield return new WaitForSeconds(1f);
        }
    }

    private void ScanForEnemies()
    {
        Debug.Log("Detected " + _detectedEnemyList.Count + " enemies");
        _detectedEnemyList.Clear();
        _closetEnemy = null;
        
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        foreach (var hit in hits)
        {
            Transform enemy = hit.transform;
            _detectedEnemyList.Add(enemy);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
