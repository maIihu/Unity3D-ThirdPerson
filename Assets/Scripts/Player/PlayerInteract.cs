using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        Ray ray = new Ray(interactPoint.position, interactPoint.forward);
        Debug.DrawRay(interactPoint.position, interactPoint.forward * distance, Color.red);
        if (Physics.Raycast(ray, out var hitInfo, distance, layerMask))
        {
            if (hitInfo.collider.TryGetComponent<IInteractable>(out var objInteract))
            {
                objInteract.Interact();
            }
        }
    }
}
