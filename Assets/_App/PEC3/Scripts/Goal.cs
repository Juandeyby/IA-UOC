using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Transform agent;
    
    private Collider _collider;
    private Renderer _renderer;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }
    
    public void DisableCollider()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(agent.position, transform.position) > 2.5f)
        {
            EnableCollider();
        }
    }
}
