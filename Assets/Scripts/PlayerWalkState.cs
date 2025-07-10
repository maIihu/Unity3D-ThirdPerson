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
        Move();
    }
    
    private void Move()
    {
        // Lấy hướng camera
        Vector3 camForward = Player.mainCamera.transform.forward;
        Vector3 camRight = Player.mainCamera.transform.right;
    
        // Loại bỏ trục Y (tránh nhân vật bay lên trời)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
    
        // Tính hướng di chuyển theo hướng camera
        Vector3 moveDir = camForward * Player.moveInput.y + camRight * Player.moveInput.x;
        moveDir.Normalize();
    
        // Di chuyển
        Player.transform.position += moveDir * (Player.moveSpeed * Time.deltaTime);
    
        // Xoay nhân vật theo hướng di chuyển
        if (moveDir != Vector3.zero)
        {
            Player.transform.forward = moveDir;
        }
    }
}
