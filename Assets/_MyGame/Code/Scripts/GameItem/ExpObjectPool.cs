
using System.Collections.Generic;
using UnityEngine;

public class ExpObjectPool : MonoBehaviour
{
    private static ExpObjectPool _instance;
    public static ExpObjectPool Instance { get { return _instance; } }
    
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private int expPoolSize;
    
    private List<GameObject> _expPool;

    private void Awake()
    {
        if(_instance != null &&  _instance != this)
            Destroy(this.gameObject);
        else 
            _instance = this;
    }

    private void Start()
    {
        _expPool = new List<GameObject>();
        for (int i = 0; i < expPoolSize; i++)
        {
            GameObject expObj = Instantiate(expPrefab, this.transform);
            expObj.SetActive(false);
            _expPool.Add(expObj);
        }
    }

    public GameObject GetExpObject()
    {
        foreach (GameObject expObj in _expPool)
        {
            if (!expObj.activeInHierarchy)
            {
                expObj.SetActive(true);
                return expObj;
            }
        }
        GameObject newExpObj = Instantiate(expPrefab, this.transform);
        _expPool.Add(newExpObj);
        return newExpObj;
    }

    public void ReturnExpObject(GameObject expObj)
    {
        expObj.SetActive(false);
    }
}
