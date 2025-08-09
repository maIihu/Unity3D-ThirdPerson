using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    private float countdown = 5f;
    public BossIdleState(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            StateMachine.ChangeState(Boss.MeteorSkill);
        }
    }
}
