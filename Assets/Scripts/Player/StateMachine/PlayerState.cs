using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
   protected Player Player;
   protected PlayerStateMachine StateMachine;
   
   private string _animBoolName;

   protected float StartTime;
   public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
   {
      this.StateMachine = stateMachine;
      this.Player = player;
      this._animBoolName = animBoolName;
   }

   public virtual void Enter()
   {
      Player.Anim.SetBool(_animBoolName, true);
      StartTime = Time.time;
   }

   public virtual void Exit()
   {
      Player.Anim.SetBool(_animBoolName, false);
   }

   public virtual void LogicUpdate()
   {
      
   }

   public virtual void PhysicsUpdate()
   {
      
   }
}
