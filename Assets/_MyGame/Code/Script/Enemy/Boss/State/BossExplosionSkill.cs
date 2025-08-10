
using UnityEngine;

public class BossExplosionSkill : BossAttackState
{
    public BossExplosionSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.TriggerExplosionSkill();
        DelayTimer = 3f;
        IsAttackComplete = true;
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
