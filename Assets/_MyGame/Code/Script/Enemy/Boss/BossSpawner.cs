
using System;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float timeToSpawn;

    private void Update()
    {
        if(GameManager.Instance.GameTimer >= timeToSpawn) Spawn();
    }

    private void Spawn()
    {
        Instantiate(bossPrefab, this.transform.position, Quaternion.identity, this.transform);
    }
}
