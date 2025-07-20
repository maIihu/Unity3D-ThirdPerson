using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData data;
    
    [Header("Interact")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private LayerMask layerMask;
    
    [Header("UI")]
    [SerializeField] private UIExpBar expBar;
    [SerializeField] private UIManager uiManager;
    
    private CharacterController _controller;

    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    
    private float _gravity;
    private float _gravityMultiplier;

    private float _currentExp;
    private float _currentLevel;
    private float _maxExp;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _gravity = -9.8f;
        _gravityMultiplier = 3;
        _maxExp = data.exp;
        _currentLevel = data.level;
        expBar.UpdateBarUI(_currentExp, _maxExp, _currentLevel);
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
        _controller.Move(transform.TransformDirection(_moveDirection) * (data.moveSpeed * Time.deltaTime));
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
            _playerVelocity.y = Mathf.Sqrt(data.jumpForce * -2f * _gravity);
        }
    }

    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(interactPoint.position, data.interactDistance, layerMask);

        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent<IInteractable>(out var objInteract))
            {
                objInteract.Interact(this);
                break; 
            }
        }

        // DrawCircle(interactPoint.position, data.interactDistance, Color.yellow);
    }

    private void DrawCircle(Vector3 center, float radius, Color color, int segments = 32)
    {
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        for (int i = 1; i <= segments; i++)
        {
            angle += 2 * Mathf.PI / segments;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Debug.DrawLine(lastPoint, nextPoint, color);
            lastPoint = nextPoint;
        }
    }
    
    public void Exp(float exp)
    {
        _currentExp += exp;
        expBar.UpdateBarUI(_currentExp, _maxExp, _currentLevel);
        if (_currentExp >= _maxExp)
        {
            _currentLevel++;
            _currentExp -=  _maxExp;
            expBar.UpdateBarUI(_currentExp, _maxExp, _currentLevel);
            uiManager.PlayerLevelUp();
        }
    }
}
