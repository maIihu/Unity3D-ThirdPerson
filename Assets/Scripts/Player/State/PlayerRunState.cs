using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerMoveState
{
    private float _runTimer;
    private float _runMaxTimer = 3;
    
    public PlayerRunState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _runTimer = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Player.moveInput == Vector2.zero)
        {
            Player.StateMachine.ChangeState(Player.IdleState);
        }
        Move(Player.moveSpeed * 2);
        
        HandleRunStateDuration();
        if (_runTimer >= _runMaxTimer)
        {
            Player.StateMachine.ChangeState(Player.WalkState);
        }
    }

    private void HandleRunStateDuration()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            _runTimer = 0f;
        }
        _runTimer += Time.deltaTime;
    }
}
