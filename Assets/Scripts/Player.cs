using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public PlayerIdleState IdleState;
    public PlayerWalkState WalkState;
    public PlayerStateMachine StateMachine;
    
    [SerializeField] public float moveSpeed = 7f;

    public Animator anim;
    public Camera mainCamera;
    public Vector2 moveInput;

    private void Awake()
    {
        mainCamera = Camera.main; 
        anim = GetComponentInChildren<Animator>();
        if(anim == null) Debug.LogError("No animator found");
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine();
        this.IdleState = new PlayerIdleState(StateMachine, this, "IsIdling");
        this.WalkState = new PlayerWalkState(StateMachine, this, "IsWalking");
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        InputHandler();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void InputHandler()
    {
        moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveInput.y += 1;
        if (Input.GetKey(KeyCode.A)) moveInput.x -= 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y -= 1;
        if (Input.GetKey(KeyCode.D)) moveInput.x += 1;
        moveInput.Normalize();
    }


    
}