
using UnityEngine;

public class BossExplosionSkill : BossAttackState
{
    private float _timer;
    public BossExplosionSkill(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Boss.StartExplosionSkill();
        IsAttackComplete = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Debug.Log("No xong");
        }
    }
}
