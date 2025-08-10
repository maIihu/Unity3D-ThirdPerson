
using UnityEngine;

public class BossFireballSkill : BossAttackState
{
    public BossFireballSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.TriggerFireballSkill();
        DelayTimer = 3f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Boss.HasFireballAttackEnded())
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
