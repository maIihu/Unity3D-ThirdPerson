using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI_System : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator indicatorPrefab;
    [SerializeField] private RectTransform holder;
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform player;
    
    private Dictionary<int, DamageIndicator> _indicators;

    private static DI_System _instance;
    public static DI_System Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null &&  _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        _indicators = new Dictionary<int, DamageIndicator>();
    }

    public void CreateIndicator(EnemyBase target)
    {
        if (!_indicators.ContainsKey(target.indicatorID))
        {
            DamageIndicator indicator = Instantiate(indicatorPrefab, holder);
            indicator.Register(target.transform, player);
            _indicators.Add(target.indicatorID, indicator);
        }
    }

    public bool CheckIfObjectInSight(Transform target)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(target.position);
        Debug.Log($"[{target.name}] Viewport: {screenPoint}");

        return screenPoint is { z: > 0, x: > 0 and < 1, y: > 0 and < 1 };
    }

    public bool Check(Transform target)
    {
        Vector3 playerXZ = new Vector3(player.position.x, 0, player.position.z);
        Vector3 targetXZ = new Vector3(target.position.x, 0, target.position.z);
        float distance = Vector3.Distance(targetXZ, playerXZ);
        return distance <= 5;
    }

    public void RemoveIndicator(int id)
    {
        if (_indicators.TryGetValue(id, out DamageIndicator indicator))
        {
            _indicators.Remove(id);
            if (indicator != null) Destroy(indicator.gameObject);
        }
    }
        
}
