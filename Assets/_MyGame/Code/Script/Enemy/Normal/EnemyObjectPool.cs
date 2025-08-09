
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public enum EnemyType
{
    Flying, Ground
}

public class EnemyObjectPool : MonoBehaviour
{
    private static EnemyObjectPool _instance;
    public static EnemyObjectPool Instance { get { return _instance; } }

    [System.Serializable]
    public class EnemyPoolInfo
    {
        public EnemyType poolType;
        public GameObject prefab;
        public int initialPoolSize = 30;
    }

    [SerializeField] private List<EnemyPoolInfo> enemyPools;
    
    private Dictionary<EnemyType, Queue<GameObject>> _poolQueue;
    private Dictionary<EnemyType, List<GameObject>> _prefabListLookup;
    
    private void Awake()
    {
        if(_instance != null &&  _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;
    }
    
    private void Start()
    {
        _poolQueue = new Dictionary<EnemyType, Queue<GameObject>>();
        _prefabListLookup = new Dictionary<EnemyType, List<GameObject>>();
        
        InitNewPool();
    }

    private void InitNewPool()
    {
        foreach (var pool in enemyPools)
        {
            if (!_prefabListLookup.ContainsKey(pool.poolType))
            {
                _prefabListLookup[pool.poolType] = new List<GameObject>();
            }
            _prefabListLookup[pool.poolType].Add(pool.prefab);
            if (!_poolQueue.ContainsKey(pool.poolType))
            {
                _poolQueue[pool.poolType] = new Queue<GameObject>();
            }
            for (int i = 0; i < pool.initialPoolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                _poolQueue[pool.poolType].Enqueue(obj);
            }
        }
    }

    public GameObject GetEnemyObject(EnemyType poolType)
    {
        if(!_poolQueue.ContainsKey(poolType)) return null;
        GameObject obj;
        if (_poolQueue[poolType].Count > 0)
        {
            obj = _poolQueue[poolType].Dequeue();
        }
        else
        {
            List<GameObject> prefabList = _prefabListLookup[poolType];
            int randIndex = Random.Range(0, prefabList.Count);
            GameObject randomPrefab = prefabList[randIndex];

            obj = Instantiate(randomPrefab, transform);
        }
        obj.SetActive(true);
        return obj;
    }

    public void ReturnEnemyObject(EnemyType poolType, GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        _poolQueue[poolType].Enqueue(enemyObj);
    }
}
