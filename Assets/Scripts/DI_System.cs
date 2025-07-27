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
    
    private Dictionary<Transform, DamageIndicator> indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates

    public static Action<Transform> CreateIndicator = delegate{};
    public static Func<Transform, bool> CheckIfObjectInSight = null;

    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }

    private void Create(Transform target)
    {
        if (indicators.ContainsKey(target))
        {
            indicators[target].Restart();
            return;
        }
        DamageIndicator indicator = Instantiate(indicatorPrefab, holder);
        indicator.Register(target, player, new Action(() => { indicators.Remove(target);}));
        
        indicators.Add(target, indicator);
    }

    private bool InSight(Transform target)
    {
        Vector3 screentPoint = camera.WorldToViewportPoint(target.position);
        return screentPoint.z > 0 && screentPoint.x > 0 && screentPoint.x < 1 && screentPoint.y > 0 && screentPoint.y < 1;
    }
}
