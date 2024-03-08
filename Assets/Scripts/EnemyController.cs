using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 fightAreaAnchor;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        fightAreaAnchor = this.gameObject.transform.parent.position;

        MoveToPoint();
    }

    private void MoveToPoint()
    {
        Vector3 point = GetRandomPointInArea();
        agent.SetDestination(point);
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
        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToPoint();
        }
    }
}
