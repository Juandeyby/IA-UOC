using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlphaAgent : Agent
{
    [SerializeField] private Transform [] goal;
    [SerializeField] private Transform map;
    
    public override void OnEpisodeBegin()
    { 
        transform.localPosition = new Vector3(0, 0.5f, 0);
        
//        transform.localPosition = new Vector3(0, 0.5f, 0);
//        transform.localRotation = Quaternion.identity;
        foreach (var g in goal)
        {
            g.gameObject.SetActive(true);
        }
        
        // Randomize the local rotation of the agent
        transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
        // Randomize the map rotation 90 180 270
        if (map != null)
        {
            var randomRotation = Random.Range(0, 4) * 90;
            map.localRotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }
    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = actionBuffers.DiscreteActions[0];

        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        transform.position += transform.forward * Time.fixedDeltaTime * 2.0f;
        transform.position += dirToGo * Time.fixedDeltaTime * 1.5f;
        
        if (transform.localPosition.y < 0)
        {
            SetReward(-1f);
            EndEpisode();
        }
        
        AddReward(-0.0001f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goal"))
        {
            AddReward(0.01f);
            var g = other.GetComponent<Goal>();
            g.DisableCollider();
        }
        if (other.transform.CompareTag("wall"))
        {
            // SetReward(-1f);
            // EndEpisode();
            AddReward(-0.0002f);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("wall"))
        {
            // SetReward(-1f);
            // EndEpisode();
            AddReward(-0.0002f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        } 
    }
}
