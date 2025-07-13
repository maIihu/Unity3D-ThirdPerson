using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerMoveState
{
    public PlayerWalkState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Player.moveInput == Vector2.zero)
        {
            Player.StateMachine.ChangeState(Player.IdleState);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Player.StateMachine.ChangeState(Player.RunState);
        }
    
        
        Move(Player.moveSpeed);
    }
    

}
