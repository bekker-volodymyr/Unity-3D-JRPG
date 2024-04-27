using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFightPositionState : PlayerState
{
    #region Lerp Variables

    // Init on lerp start
    private float lerpStartTime;
    private Vector3 lerpStartPos;
    private float lerpLength;

    private float lerpSpeed = 5.0f;

    #endregion

    public MoveToFightPositionState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        lerpStartTime = Time.time;
        lerpStartPos = player.transform.position;
        lerpLength = Vector3.Distance(lerpStartPos, GameManager.Instance.playerFightPosition.position);
        player.SetAnimatorState(1);
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

        float distCovered = (Time.time - lerpStartTime) * lerpSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        if (fractionOfJourney >= 1f)
        {
            player.SetAnimatorState(0);
            if (GameManager.Instance.isPlayerTurn)
            {
                stateMachine.ChangeState(player.WaitToAttackTargetState);
            }
            else
            {
                stateMachine.ChangeState(player.WaitForHitState);
            }
        }
        else
        {
            player.transform.position = Vector3.Lerp(lerpStartPos, GameManager.Instance.playerFightPosition.position, fractionOfJourney);
        }
    }
}
