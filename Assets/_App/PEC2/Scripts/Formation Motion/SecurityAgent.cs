using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LeaderSecurityAgent leader;
    private Transform _leaderTransform;
    private int _key;
    private Vector3 _position;

    private void Awake()
    {
        _leaderTransform = leader.transform;
    }
    
    private void OnEnable()
    {
        _key = leader.GetAgentKey(this);
        _position = leader.GetFormationPosition(_key);
    }

    private void Update()
    {
        agent.destination = _leaderTransform.TransformPoint(_position);
        agent.transform.LookAt(agent.destination);
    }
    
    private void OnDisable()
    {
        leader.RemoveAgent(_key);
    }
}
