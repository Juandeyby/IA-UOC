using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[DefaultExecutionOrder(-1)]
public class LeaderSecurityAgent : MonoBehaviour
{
    [SerializeField] private Transform movementGhost;
    [SerializeField] private NavMeshAgent agent;
    private Dictionary<int, Vector3> _positions = new Dictionary<int, Vector3>();
    private Dictionary<int, SecurityAgent> _agents = new Dictionary<int, SecurityAgent>();
    private int _row = 3;
    private int _column = 3;
    private float _distance = 1.25f;

    private void OnEnable()
    {
        SetUpFormation();
    }

    /// <summary>
    /// Creacion de la formacion segun el numero de filas y columnas
    /// </summary>
    private void SetUpFormation()
    {
        var key = 0;
        for (var i = _row - 1; i >= 0; i--)
        {
            for (var j = 0; j < _column; j++)
            {
                var distanceToCenterColumn = (_column - 1) * _distance / 2f;
                var distanceToCenterRow = (_row) * _distance;
                _positions[key] = new Vector3((j * _distance) - distanceToCenterColumn, 0, (i * _distance) - distanceToCenterRow);
                key++;
            }
        }
    }

    /// <summary>
    /// Recuperar la clave del agente
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    public int GetAgentKey(SecurityAgent agent)
    {
        if (_agents.ContainsValue(agent))
        {
            return _agents.FirstOrDefault(x => x.Value == agent).Key;
        }

        var max = _row * _column;
        for (var i = 0; i < max; i++)
        {
            if (_agents.TryAdd(i, agent))
            {
                return i;
            }
        }
        return -1;
    }
    
    /// <summary>
    /// Recuperar la posicion de la formacion
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Vector3 GetFormationPosition(int key)
    {
        return _positions[key];
    }
    
    /// <summary>
    /// Remover agente de la formacion
    /// </summary>
    /// <param name="key"></param>
    public void RemoveAgent(int key)
    {
        _agents.Remove(key);
    }

    /// <summary>
    /// Actualizar la posicion del lider al movimiento del fantasma
    /// </summary>
    private void Update()
    {
        agent.destination = movementGhost.transform.position;
    }
}
