using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForHitState : PlayerState
{
    public WaitForHitState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        GameManager.Instance.EnemiesTurn?.Invoke();
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
    }
}
