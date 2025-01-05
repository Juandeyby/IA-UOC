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
        // Obtenemos la referencia al transform del lider
        _leaderTransform = leader.transform;
    }
    
    private void OnEnable()
    {
        // Añadimos el agente a la formacion y obtenemos su clave
        _key = leader.GetAgentKey(this);
        // Obtenemos la posicion en la formacion
        _position = leader.GetFormationPosition(_key);
    }

    private void Update()
    {
        // Movemos el agente a la posicion en la formacion
        agent.destination = _leaderTransform.TransformPoint(_position);
        // Rotamos el agente hacia la posicion de destino
        agent.transform.LookAt(agent.destination);
    }
    
    private void OnDisable()
    {
        // Eliminamos el agente de la formacion
        leader.RemoveAgent(_key);
    }
}
