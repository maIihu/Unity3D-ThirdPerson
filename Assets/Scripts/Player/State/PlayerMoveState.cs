using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    private Vector3 GetCameraDirection()
    {
        // Lấy hướng camera
        Vector3 camForward = Player.MainCamera.transform.forward;
        Vector3 camRight = Player.MainCamera.transform.right;
    
        // Loại bỏ trục Y (tránh nhân vật bay lên trời)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        return camForward * Player.moveInput.y + camRight * Player.moveInput.x;
    }
    
    protected void Move(float speed)
    {
        var moveDir = GetCameraDirection();
        // Tính hướng di chuyển theo hướng camera
        moveDir.Normalize();
    
        // Di chuyển
        //Player.transform.position += moveDir * (speed * Time.deltaTime);
        Player.CharacterControl.Move(moveDir * (speed * Time.deltaTime));
    
        // Xoay nhân vật theo hướng di chuyển
        if (moveDir != Vector3.zero)
        {
            Player.transform.forward = moveDir;
        }
    }
}
