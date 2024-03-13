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

    private float rotationSpeed = 3.0f;

    private Enums.State state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        fightAreaAnchor = fightArea.transform.position;
        fightPosition = transform.position;
        fightRotation = transform.rotation;

        state = Enums.State.Idle;

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
        if (!agent.pathPending && agent.remainingDistance < 0.1f && state == Enums.State.Idle)
        {
            MoveToRandomPoint();
        }
    }

    public void ChangeState(Enums.State newState)
    {
        if(newState == Enums.State.Fight && state != Enums.State.Fight)
        {
            state = newState;
            MoveToFightPosition();
            RotateTowards(player.transform.position);
        }
    }

    public void RotateTowards(Vector3 position)
    {
        Vector3 directionToPlayer = position - transform.position;
        var targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
