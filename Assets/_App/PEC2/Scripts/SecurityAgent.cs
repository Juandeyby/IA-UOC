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
    [SerializeField] private int _key;
    
    [SerializeField] private Vector3 _position;
    private Quaternion _rotation;

    private void Awake()
    {
        _leaderTransform = leader.transform;
    }
    
    private void OnEnable()
    {
        _key = leader.GetAgentKey(this);
        _position = leader.GetFormationPosition(_key);
    }

    private void Start()
    {
        _rotation = Quaternion.Inverse(_leaderTransform.rotation) * transform.rotation;
    }

    private void Update()
    {
        agent.destination = _leaderTransform.TransformPoint(_position);
        agent.transform.rotation = _leaderTransform.rotation * _rotation;
    }
    
    private void OnDisable()
    {
        leader.RemoveAgent(_key);
    }
}
