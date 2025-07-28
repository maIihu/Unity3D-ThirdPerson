using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rect;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }
    
    public Transform Target { get; protected set; } = null;
    private Transform Player = null;
    
    private Quaternion tRot = Quaternion.identity;
    private Vector3 tPos = Vector3.zero;

    public void Register(Transform target, Transform player)
    {
        this.Target = target;   
        this.Player = player;
        StartCoroutine(RotateToThePlayer());
    }
    
    private IEnumerator RotateToThePlayer()
    {
        while (enabled)
        {
            if (Target == null || !Target.gameObject.activeInHierarchy)
                yield break;
            
            if (Target)
            {
                tPos = Target.position;
                tRot = Target.rotation;
            }
            Vector3 direction = Player.position - tPos;
            tRot = Quaternion.LookRotation(direction);
            tRot.z = -tRot.y;
            tRot.x = 0;
            tRot.y = 0;

            Vector3 northDirection = new Vector3(0, 0, Player.eulerAngles.y);
            rect.localRotation = tRot * Quaternion.Euler(northDirection);
            yield return null;
        }
    }
    
    
    
}
