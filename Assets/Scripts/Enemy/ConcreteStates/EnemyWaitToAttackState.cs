using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitToAttackState : EnemyState
{
    private bool isHovered = false;

    public EnemyWaitToAttackState(EnemyStateMachine stateMachine, EnemyController enemy) : base(stateMachine, enemy)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy wait to attack state enter");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameLateUpdate()
    {
        base.FrameLateUpdate();

        enemy.ToggleMarker(isHovered);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform raycast and check if it hits a GameObject
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit GameObject is the one you're interested in
            if (hit.collider.gameObject != enemy.gameObject)
            {
                isHovered = false;
            }
            else
            {
                isHovered = true;
            }

            if (Input.GetMouseButtonDown(0) && isHovered)
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        // Implement your click logic here
        Debug.Log("Object clicked: " + enemy.name);
        GameManager.Instance.AttackEnded += OnPlayerAttackEnded;
        GameManager.Instance.Attack(enemy.gameObject);
    }

    private void OnPlayerAttackEnded()
    {
        GameManager.Instance.AttackEnded -= OnPlayerAttackEnded;
        enemy.Death();
    }
}
