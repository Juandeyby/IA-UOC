using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Muse.Behavior;

[Serializable]
[Condition(name: "Distance", story: "Is [Leader] close to [Enemies] [Value]", category: "Variable Conditions", id: "bcfc2748f58195a6dc5151e74aa0518d")]
public class Distance : Condition
{
    public BlackboardVariable<GameObject> Leader;
    public BlackboardVariable<List<GameObject>> Enemies;
    public BlackboardVariable<int> Value;

    public override bool IsTrue()
    {
        Enemies.Value.Clear();
        var hitColliders = Physics.OverlapSphere(Leader.Value.transform.position, Value.Value);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("security"))
            {
                Enemies.Value.Add(hitCollider.gameObject);
                return true;
            }
        }
        return false;
    }

    public override void OnNodeStart()
    {
    }

    public override void OnNodeEnd()
    {
    }
}
