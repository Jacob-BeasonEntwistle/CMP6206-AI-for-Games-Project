using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ReturnToHive", story: "[Bee] return to [Hive] at [speed]", category: "Action", id: "6c2311a61b31f41403d60065bcf290dd")]
public partial class ReturnToHiveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Hive;
    [SerializeReference] public BlackboardVariable<float> Speed;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector3.Distance(Bee.Value.transform.position, Hive.Value.transform.position);

        if (distance > 1.0f)
        {
            Bee.Value.transform.position = Vector3.MoveTowards(
                Bee.Value.transform.position,
                Hive.Value.transform.position,
                Speed.Value * Time.deltaTime
            );

            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

