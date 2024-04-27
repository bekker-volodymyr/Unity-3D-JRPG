using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyStateMachine stateMachine, EnemyController enemy) : base(stateMachine, enemy)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log($"{enemy.name} attack state enter");
        Vector3 targetPosition = GameManager.Instance.playerFightPosition.position;
        Vector3 attackPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z + 1.6f);
        enemy.agent.SetDestination(attackPosition);
    }
    public override void ExitState()
    {
        base.ExitState();

        enemy.SetAnimationState(0);
        //GameManager.Instance.isPlayerTurn = true;
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
            //GameManager.Instance.EnemyAttack?.Invoke();
            enemy.SetAnimationState(1);
        }
    }
}

