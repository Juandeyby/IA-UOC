using System;
using UnityEngine;
using Unity.Muse.Behavior;
using UnityEngine.Serialization;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "MovementToTree", story: "[Leader] follows [Hive]", category: "Action", id: "d3ec858a7a39922d19aa241bfeb36a55")]
public class MovementToTree : Action
{
    public BlackboardVariable<GameObject> Leader;
    public BlackboardVariable<GameObject> Hive;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Vector3.Distance(Leader.Value.transform.position, Hive.Value.transform.position) < 0.1f)
        {
            return Status.Success;
        }
        var direction = (Hive.Value.transform.position - Leader.Value.transform.position).normalized;
        var newPosition = Leader.Value.transform.position + direction * (5 * Time.deltaTime);
        newPosition.y = Mathf.Max(newPosition.y, 2.0f);
        Leader.Value.transform.position = newPosition;
        var rotation = Quaternion.LookRotation(direction);
        Leader.Value.transform.rotation = Quaternion.Slerp(Leader.Value.transform.rotation, rotation, 5 * Time.deltaTime);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

