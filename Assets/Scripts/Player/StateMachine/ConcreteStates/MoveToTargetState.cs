using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveToTargetState : PlayerState
{

    private float lerpStartTime;
    private Vector3 lerpStartPos;
    private float lerpLength;
    private Vector3 targetPosition;
    private Vector3 positionForAttack;

    private float lerpSpeed = 5.0f;
    public MoveToTargetState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        lerpStartTime = Time.time;
        lerpStartPos = player.transform.position;
        targetPosition = GameManager.Instance.attackTarget.transform.position;
        positionForAttack = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z - 1.6f);
        lerpLength = Vector3.Distance(lerpStartPos, positionForAttack);
        player.SetAnimatorState(1);
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

        float distCovered = (Time.time - lerpStartTime) * lerpSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        if (fractionOfJourney >= 1f)
        {
            player.SetAnimatorState(0);
            stateMachine.ChangeState(player.AttackState);
        }
        else
        {
            player.transform.position = Vector3.Lerp(lerpStartPos, positionForAttack, fractionOfJourney);
        }
    }
}
