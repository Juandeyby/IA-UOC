using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "MovementToEnemies", story: "[Agent] follows [Enemies]", category: "Action", id: "af2be5968448699b050a15b65954d079")]
public class MovementToEnemies : Action
{
    public BlackboardVariable<GameObject> Agent;
    public BlackboardVariable<List<GameObject>> Enemies;
    protected override Status OnStart()
    {
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Enemies.Value.Count == 0)
        {
            return Status.Failure;
        }
        var hitCollider = Enemies.Value[0].GetComponent<Collider>();
        if (Vector3.Distance(Agent.Value.transform.position, hitCollider.transform.position) < 1f)
        {
            return Status.Success;
        }
        var direction = (hitCollider.transform.position - Agent.Value.transform.position).normalized;
        Agent.Value.transform.position += direction * (5 * Time.deltaTime);
        var rotation = Quaternion.LookRotation(direction);
        Agent.Value.transform.rotation = Quaternion.Slerp(Agent.Value.transform.rotation, rotation, 5 * Time.deltaTime);

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

