
using UnityEngine;

public class BossMeteorSkill : BossAttackState
{
    public BossMeteorSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.ActiveMeteorEffect();
    }
}
