
public class BossFireBallSkill : BossAttackState
{
    public BossFireBallSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.StartFireBallSkill();
        //IsAttackComplete = true;
    }
}
