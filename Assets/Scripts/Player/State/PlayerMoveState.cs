using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    private float _currentVelocity;
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }
    
    protected void Move(float speed)
    {
        Player.moveDirection = new Vector3(Player.moveInput.x, 0f, Player.moveInput.y);
        ApplyRotation();
        Player.CharacterControl.Move(Player.moveDirection * (speed * Time.deltaTime));
    }
    
    private void ApplyRotation()
    {
        if (Player.moveInput.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(Player.moveDirection.x, Player.moveDirection.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(Player.transform.eulerAngles.y, targetAngle, ref _currentVelocity, 0.1f);
        Player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
    }
}
