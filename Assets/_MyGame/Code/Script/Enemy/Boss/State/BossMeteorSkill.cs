
using UnityEngine;

public class BossMeteorSkill : BossAttackState
{
    public BossMeteorSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.TriggerMeteorSkill();
        DelayTimer = 6f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DelayTimer -= Time.deltaTime;
        if (DelayTimer <= 0)
        {
            StateMachine.ChangeState(Boss.FireballSkill);
        }
    }
}
