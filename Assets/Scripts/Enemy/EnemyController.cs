using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject marker;

    [SerializeField] private GameObject fightArea;

    private Animator animator;

    public NavMeshAgent agent;
    private Vector3 fightAreaAnchor;
    private Vector3 fightPosition;

    #region Slerp Variables
    
    Vector3 directionToPlayer;
    // Init on lerp start
    private float slerpStartTime;
    Quaternion startRotation;
    
    Quaternion targetRotation;

    private float slerpLength;

    private float rotationSpeed = 3.0f;

    #endregion

    #region State Machine
    public EnemyStateMachine EnemyStateMachine { get; set; }
    public EnemyIdleState EnemyIdleState { get; set; }
    public EnemyMoveToFightPositionState EnemyMoveToFightPosState { get; set; }
    public EnemyWaitToAttackState EnemyWaitToAttackState { get; set; }
    public EnemyAttackState EnemyAttackState { get; set; }
    public EnemyTurnState  EnemyTurnState { get; set; }
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        fightAreaAnchor = fightArea.transform.position;
        fightPosition = transform.position;

        EnemyStateMachine = new EnemyStateMachine();
        EnemyIdleState = new EnemyIdleState(EnemyStateMachine, this);
        EnemyMoveToFightPosState = new EnemyMoveToFightPositionState(EnemyStateMachine, this);
        EnemyWaitToAttackState = new EnemyWaitToAttackState(EnemyStateMachine, this);
        EnemyAttackState = new EnemyAttackState(EnemyStateMachine, this);
        EnemyTurnState = new EnemyTurnState(EnemyStateMachine, this);

        EnemyStateMachine.Init(EnemyIdleState);
    }

    public void MoveToRandomPoint()
    {
        Vector3 point = GetRandomPointInArea();
        agent.SetDestination(point);
    }
    public void MoveToFightPosition()
    { 
        agent.SetDestination(fightPosition);
    }
    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;

        randomDirection += fightAreaAnchor;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);

        return hit.position;
    }

    void Update()
    {
        EnemyStateMachine.CurrentState.FrameUpdate();
    }

    private void LateUpdate()
    {
        EnemyStateMachine.CurrentState.FrameLateUpdate();
    }

    public void ToggleMarker(bool isHovered)
    {
        marker.SetActive(isHovered);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void SetAnimationState(int state)
    {
        animator.SetInteger("State", state);
    }

    public void OnAttackEnds()
    {
        EnemyStateMachine.ChangeState(EnemyMoveToFightPosState);
    }
}
