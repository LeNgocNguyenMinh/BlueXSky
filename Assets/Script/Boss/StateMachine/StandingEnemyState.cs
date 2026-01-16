using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyState 
{
    protected Boss boss;
    protected StandingEnemyStateMachine stateMachine;

    public StandingEnemyState(Boss boss, StandingEnemyStateMachine stateMachine)
    {
        this.boss = boss;
        this.stateMachine = stateMachine;
    }
    public virtual void EnterState()
    {
        
    }
    public virtual void ExitState()
    {
        
    }
    public virtual void FrameUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
    {
        
    }
}
