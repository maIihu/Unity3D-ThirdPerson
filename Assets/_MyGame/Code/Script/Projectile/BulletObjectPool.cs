using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletPrefab;
    [SerializeField] private int bulletPoolSize;
    
    private List<GameObject> _bulletPool;
    
    private void Start()
    {
        _bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            int indexRandom = Random.Range(0, bulletPrefab.Length);
            GameObject bulletObj = Instantiate(bulletPrefab[indexRandom], this.transform);
            bulletObj.SetActive(false);
            _bulletPool.Add(bulletObj);
        }
    }

    public GameObject GetBulletObject()
    {
        foreach (GameObject bulletObj in _bulletPool)
        {
            if (!bulletObj.activeInHierarchy)
            {
                bulletObj.SetActive(true);
                return bulletObj;
            }
        }
        int indexRandom = Random.Range(0, bulletPrefab.Length);
        GameObject newBulletObj = Instantiate(bulletPrefab[indexRandom], this.transform);
        _bulletPool.Add(newBulletObj);
        return newBulletObj;
    }

    public void ReturnBulletObject(GameObject bulletObj)
    {
        bulletObj.SetActive(false);
    }
}
