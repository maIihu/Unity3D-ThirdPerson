
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestDetected : MonoBehaviour
{
    [SerializeField] private EnemyBase enemy;
    private void OnEnable()
    {
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        while (true)
        {
            if (!DI_System.Instance.CheckIfObjectInSight(this.transform))
            {
                DI_System.Instance.CreateIndicator(enemy);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    


    private void OnDisable()
    {
        StopAllCoroutines();
        DI_System.Instance.RemoveIndicator(enemy.indicatorID);
    }
    
}
