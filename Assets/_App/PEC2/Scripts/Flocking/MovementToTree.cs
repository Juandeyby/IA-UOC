using System;
using UnityEngine;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;

[Serializable]
[NodeDescription(name: "MovementToTree", story: "[Leader] follows [Tree]", category: "Action", id: "d3ec858a7a39922d19aa241bfeb36a55")]
public class MovementToTree : Action
{
    public BlackboardVariable<GameObject> Leader;
    public BlackboardVariable<GameObject> Tree;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Vector3.Distance(Leader.Value.transform.position, Tree.Value.transform.position) < 1f)
        {
            return Status.Success;
        }
        var direction = (Tree.Value.transform.position - Leader.Value.transform.position).normalized;
        Leader.Value.transform.position += direction * (5 * Time.deltaTime);
        var rotation = Quaternion.LookRotation(direction);
        Leader.Value.transform.rotation = Quaternion.Slerp(Leader.Value.transform.rotation, rotation, 5 * Time.deltaTime);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

