using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossState
{
    public BossDeadState(BossStateMachine stateMachine, BossController boss) : base(stateMachine, boss)
    {
    }
}
