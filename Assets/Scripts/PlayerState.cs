using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
   protected Player Player;
   protected PlayerStateMachine StateMachine;

   private string _animBoolName;

   public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
   {
      this.StateMachine = stateMachine;
      this.Player = player;
      this._animBoolName = animBoolName;
   }

   public virtual void Enter()
   {
      Player.anim.SetBool(_animBoolName, true);   
   }

   public virtual void Exit()
   {
      Player.anim.SetBool(_animBoolName, false);
   }

   public virtual void LogicUpdate()
   {
      
   }

   public virtual void PhysicsUpdate()
   {
      
   }
}
