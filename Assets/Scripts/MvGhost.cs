using System;
using UnityEngine;
using UnityEngine.AI;

public class MvGhost : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] waypoints;

    int patrolWP = 0;

    void Start()
    {
        Patrol();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f) 
        {
            Patrol();
        }
    }

    void Seek(Vector3 pos)
    {
        agent.destination = pos; 
    }

    void Patrol()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        Seek(waypoints[patrolWP].transform.position);
    }
}
