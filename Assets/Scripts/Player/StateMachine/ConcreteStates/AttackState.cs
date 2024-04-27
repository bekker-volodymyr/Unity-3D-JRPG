using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackState : PlayerState
{

    public AttackState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        player.SetAnimatorState(2);
        GameManager.Instance.AttackEnded += OnAttackEnded;
    }

    public override void ExitState()
    {
        base.ExitState();

        player.SetAnimatorState(0);
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    private void OnAttackEnded()
    {
        GameManager.Instance.isPlayerTurn = false;
        stateMachine.ChangeState(player.MoveToFightPositionState);
    }
}
