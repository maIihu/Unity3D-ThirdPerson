using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected BossController Boss;
    protected BossStateMachine StateMachine;

    public BossState(BossStateMachine stateMachine, BossController boss)
    {
        StateMachine = stateMachine;
        Boss = boss;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter " + StateMachine.CurrentState.GetType().Name);
    }
    public virtual void Exit(){}
    public virtual void LogicUpdate(){}
    public virtual void PhysicsUpdate(){}
    
}
