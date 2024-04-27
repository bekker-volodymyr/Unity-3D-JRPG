using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; set; }
    public void Init(EnemyState startState)
    {
        CurrentState = startState;
        CurrentState.EnterState();
    }
    public void ChangeState(EnemyState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
