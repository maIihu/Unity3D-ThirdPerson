using System;
using UnityEngine;

public class SimpleThirdPersonCamera : MonoBehaviour
{
   [SerializeField] private Transform playerTarget;
   
   [SerializeField] private float mouseSensitivity;
   [SerializeField] private float distanceToPlayer;
   [SerializeField] private float height;
   
   [SerializeField] private float minY;
   [SerializeField] private float maxY;
   
   private float _yaw = 0f; // su quay ngang
   private float _pitch = 20f; // su lao xuong / quay doc

   private void Start()
   {
      Cursor.visible = false;
   }

   private void LateUpdate()
   {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
      float mouseY =  Input.GetAxis("Mouse Y") * mouseSensitivity;
      
      _yaw += mouseX;
      _pitch -= mouseY;
      _pitch = Mathf.Clamp(_pitch, minY, maxY);
      
      Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
      
      Vector3 offset = rotation * new Vector3(0.0f, 0.0f, -distanceToPlayer);
      transform.position = playerTarget.position + Vector3.up * height + offset;

      transform.LookAt(playerTarget);
   }
}