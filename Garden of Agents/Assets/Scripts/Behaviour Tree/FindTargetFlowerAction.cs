using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTargetFlower", story: "[Bee] find [flower]", category: "Action", id: "7e5898800f43286d85db96201d72d818")]
public partial class FindTargetFlowerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Flower;
    protected override Status OnStart()
    {
        if (Bee == null) return Status.Failure;

        if (Flower.Value == null)
        {
            Flower.Value = GameObject.FindWithTag("Flower");
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

