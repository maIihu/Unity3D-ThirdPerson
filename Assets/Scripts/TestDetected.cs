
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestDetected : MonoBehaviour
{
    [Range(5, 30)] [SerializeField] private float destroyTimer = 15;

    // private void OnEnable()
    // {
    //     Invoke("Register", Random.Range(0, 8));
    // }

    private void Update()
    {
        Register();
    }

    private void Register()
    {
        if (!DI_System.CheckIfObjectInSight(this.transform))
        {
            DI_System.CreateIndicator(this.transform);
        }
        //Destroy(this.gameObject, destroyTimer);
    }
    
}
