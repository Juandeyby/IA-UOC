using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvCop : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject target;

    void Update()
    {
        Seek(target.transform.position);
    }

    void Seek(Vector3 pos)
    {
        agent.destination = pos; 
    }
}
