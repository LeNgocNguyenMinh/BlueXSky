using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossATK2State : StandingEnemyState
{
    private int atkCount;
    private float atkDelay; 
    public BossATK2State(Boss boss, StandingEnemyStateMachine stateMachine) : base(boss, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        atkCount = 0;
        atkDelay = boss.ATK1Delay;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(atkCount < boss.ATK1RepeatTime)
        {
            if(atkDelay > 0f)
            {
                atkDelay -= Time.deltaTime;
                return;
            }
            atkDelay = boss.ATK1Delay;
            boss.BossATK2();
            atkCount++;
        }
        else
        {
            stateMachine.ChangeState(boss.ATK3State);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

    }
}
