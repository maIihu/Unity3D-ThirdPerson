
using UnityEngine;

public class BossFireBallSkill : BossAttackState
{
    public BossFireBallSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.StartFireBallSkill();
        DelayTimer = 3f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Boss.FireballAttackEnd())
        {
            IsAttackComplete = true;
            DelayTimer -= Time.deltaTime;
        }
        
        if (DelayTimer <= 0)
        {
            StateMachine.ChangeState(Boss.MeteorSkill);
        }
    }
}
