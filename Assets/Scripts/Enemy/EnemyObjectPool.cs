
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private static EnemyObjectPool _instance;
    public static EnemyObjectPool Instance { get { return _instance; } }
    
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private int enemyPoolSize;
    
    private List<GameObject> _enemyPool;
    
    
    private void Awake()
    {
        if(_instance != null &&  _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;
    }
    
    private void Start()
    {
        _enemyPool = new List<GameObject>();
        for (int i = 0; i < enemyPoolSize; i++)
        {
            int indexRandom = Random.Range(0, enemyPrefab.Length);
            GameObject enemyObj = Instantiate(enemyPrefab[indexRandom], this.transform);
            enemyObj.SetActive(false);
            _enemyPool.Add(enemyObj);
        }
    }

    public GameObject GetEnemyObject()
    {
        foreach (GameObject enemyObj in _enemyPool)
        {
            if (!enemyObj.activeInHierarchy)
            {
                enemyObj.SetActive(true);
                return enemyObj;
            }
        }
        int indexRandom = Random.Range(0, enemyPrefab.Length);
        GameObject newEnemyObj = Instantiate(enemyPrefab[indexRandom], this.transform);
        _enemyPool.Add(newEnemyObj);
        return newEnemyObj;
    }

    public void ReturnEnemyObject(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
    }
}
