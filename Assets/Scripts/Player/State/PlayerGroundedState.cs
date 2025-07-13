using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private const float Gravity = -9.8f;
    private float _verticalVelocity;
    public PlayerGroundedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ApplyGravity();
        Player.CharacterControl.Move(Player.moveDirection * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (Player.CharacterControl.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -1;
        }
        else
        {
            _verticalVelocity += Gravity * Player.gravityMultiplier * Time.deltaTime;
        }

        Player.moveDirection.y = _verticalVelocity;
    }
}
