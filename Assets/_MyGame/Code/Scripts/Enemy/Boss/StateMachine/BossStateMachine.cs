using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    public BossState CurrentState { get; private set; }

    public void Initialize(BossState enemyState)
    {
        CurrentState = enemyState;
        CurrentState.Enter();
    }

    public void ChangeState(BossState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
