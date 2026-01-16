using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyStateMachine
{
    public StandingEnemyState CurrentEnemyState { get; private set; }
    public void Initialize(StandingEnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }
    public void ChangeState(StandingEnemyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        Debug.Log("State changed to: " + CurrentEnemyState.GetType().Name);
        CurrentEnemyState.EnterState();
    }
}
