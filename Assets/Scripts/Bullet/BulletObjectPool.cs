using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    private static BulletObjectPool _instance;
    public static BulletObjectPool Instance { get { return _instance; } }
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletPoolSize;
    
    private List<GameObject> _bulletPool;

    private void Awake()
    {
        if(_instance != null &&  _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;
    }

    private void Start()
    {
        _bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, this.transform);
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
        GameObject newBulletObj = Instantiate(bulletPrefab, this.transform);
        _bulletPool.Add(newBulletObj);
        return newBulletObj;
    }

    public void ReturnBulletObject(GameObject bulletObj)
    {
        bulletObj.SetActive(false);
    }
}
