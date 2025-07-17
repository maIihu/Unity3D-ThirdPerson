using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    [Header("Interact")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
    
    private CharacterController _controller;

    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    
    private float _gravity;
    private float _gravityMultiplier;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _gravity = -9.8f;
        _gravityMultiplier = 3;
    }

    private void Update()
    {
        InputHandler();
        Jump();
        _moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        _playerVelocity.y += _gravity * Time.deltaTime;
        if(_controller.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2;
        }
        else
        {
            _playerVelocity.y += _gravity * _gravityMultiplier * Time.deltaTime;
        }
        Interact();
    }
    
    private void FixedUpdate()
    {
        _controller.Move(transform.TransformDirection(_moveDirection) * (moveSpeed * Time.deltaTime));
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void InputHandler()
    {
        _moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) _moveInput.y += 1f;
        if (Input.GetKey(KeyCode.A)) _moveInput.x -= 1f;
        if (Input.GetKey(KeyCode.S)) _moveInput.y -= 1f;
        if (Input.GetKey(KeyCode.D)) _moveInput.x += 1f;
        _moveInput.Normalize();
    }
    
    private void Jump()
    {
        if (_controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * _gravity);
        }
    }

    private void Interact()
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
