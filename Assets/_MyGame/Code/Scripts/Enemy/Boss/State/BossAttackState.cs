using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    protected bool IsAttackComplete;
    protected float DelayTimer;
    public BossAttackState(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        IsAttackComplete = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsAttackComplete)
        {
            
        }
    }
}
