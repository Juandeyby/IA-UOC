using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementGhost : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;

    private int _currentIndex;

    private void Start()
    {
        Patrol();
    }

    private void Patrol()
    {
        _currentIndex = (_currentIndex + 1) % waypoints.Length;
        Seek(waypoints[_currentIndex].position);
    }

    private void Seek(Vector3 position)
    {
        agent.destination = position;
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Patrol();   
        }
    }
}
