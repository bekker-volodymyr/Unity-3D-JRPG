using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject fightArea;
    [SerializeField] private GameObject player; 

    private NavMeshAgent agent;
    private Vector3 fightAreaAnchor;
    private Vector3 fightPosition;
    private Quaternion fightRotation;

    #region Slerp Variables
    
    Vector3 directionToPlayer;
    // Init on lerp start
    private float slerpStartTime;
    Quaternion startRotation;
    
    Quaternion targetRotation;

    private float slerpLength;

    private float rotationSpeed = 3.0f;

    #endregion

    private Enums.State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        fightAreaAnchor = fightArea.transform.position;
        fightPosition = transform.position;
        fightRotation = transform.rotation;

        currentState = Enums.State.Idle;

        MoveToRandomPoint();
    }

    private void MoveToRandomPoint()
    {
        Vector3 point = GetRandomPointInArea();
        agent.SetDestination(point);
    }
    private void MoveToFightPosition()
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
        switch(currentState)
        {
            case Enums.State.Idle: IdleUpdate(); break;
            case Enums.State.Fight: FightUpdate(); break;
        }
    }

    private void LateUpdate()
    {
        switch (currentState)
        {
            case Enums.State.Idle: IdleLateUpdate(); break;
            case Enums.State.Fight: FightLateUpdate(); break;
        }
    }

    public void ChangeState(Enums.State newState)
    {
        switch (newState)
        {
            case Enums.State.Idle: break;
            case Enums.State.Fight:

                MoveToFightPosition();


                slerpStartTime = Time.time;
                startRotation = transform.rotation;

                directionToPlayer = GameManager.Instance.playerFightPosition.position - transform.position;
                targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

                slerpLength = Quaternion.Angle(startRotation, targetRotation);

                break;
            default: break;
        }
        currentState = newState;
    }

    public void RotateTowardsPlayer()
    {
        float distCovered = (Time.time - slerpStartTime) * rotationSpeed;

        float fractionOfJourney = distCovered / slerpLength;

        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);
    }

    private void IdleUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToRandomPoint();
        }
    }

    private void FightUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // RotateTowardsPlayer();
            transform.LookAt(GameManager.Instance.playerFightPosition);
        }
    }


    private void FightLateUpdate()
    {
        //RotateTowards();
    }

    private void IdleLateUpdate()
    {
        return;
    }
}
