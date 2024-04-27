using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            player.SetAnimatorState(1);
            player.IdleMove(direction);
        }
        else
        {
            player.SetAnimatorState(0);
        }
    }
}
