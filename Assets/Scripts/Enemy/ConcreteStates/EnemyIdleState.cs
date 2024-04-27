using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine stateMachine, EnemyController enemy) : base(stateMachine, enemy)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        GameManager.Instance.FightInitiationEvent += OnFightInitiation;

        enemy.MoveToRandomPoint();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.1f)
        {
            enemy.MoveToRandomPoint();
        }
    }

    private void OnFightInitiation()
    {
        stateMachine.ChangeState(enemy.EnemyMoveToFightPosState);
    }
}
