using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToFlower", story: "Move [Bee] to [Flower] at [Speed]", category: "Action", id: "27c3e3caad5f9dbaef6e50d759121d3f")]
public partial class MoveToFlowerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Flower;
    [SerializeReference] public BlackboardVariable<float> Speed;
    protected override Status OnStart()
    {
        if (Flower.Value == null)
        {
            Flower.Value = GameObject.FindWithTag("Flower");
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector3.Distance(Bee.Value.transform.position, Flower.Value.transform.position);

        if (distance > 1.0f)
        {
            Bee.Value.transform.position = Vector3.MoveTowards(
                Bee.Value.transform.position,
                Flower.Value.transform.position,
                Speed.Value * Time.deltaTime
            );

            // Need this to prevent the action to exit early (resulting in no movement from the bee)
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

