using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToFightPositionState : EnemyState
{
    public EnemyMoveToFightPositionState(EnemyStateMachine stateMachine, EnemyController enemy) : base(stateMachine, enemy)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.MoveToFightPosition();
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
            enemy.transform.LookAt(GameManager.Instance.playerFightPosition.position);

            if(!GameManager.Instance.isPlayerTurn)
            {
                GameManager.Instance.isPlayerTurn = true;
                GameManager.Instance.PassTurn?.Invoke();
            }

            //Vector3 direction = GameManager.Instance.playerFightPosition.position - enemy.transform.position;
            //
            //direction.Normalize();
            //
            //float dotProduct = Vector3.Dot(enemy.transform.position, direction);
            //
            //float threshold = 0.1f;
            //
            //if (dotProduct < threshold)
            //{
                stateMachine.ChangeState(enemy.EnemyWaitToAttackState);
            //}
        }
    }
}
