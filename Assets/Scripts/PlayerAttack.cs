using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new  LayerMask();
    [SerializeField] private Transform debugTransform;
    
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("Attack");
        }
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = hit.point;
        }
        
    }
}
