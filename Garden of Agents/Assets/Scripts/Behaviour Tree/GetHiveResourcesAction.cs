using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetHiveResources", story: "Get [HiveNectarCount] and [HivePollenCount] from [Beehive]", category: "Action", id: "bcb37c116e62d51792bfd4ad7c9f1991")]
public partial class GetHiveResourcesAction : Action
{
    [SerializeReference] public BlackboardVariable<int> HiveNectarCount;
    [SerializeReference] public BlackboardVariable<int> HivePollenCount;
    [SerializeReference] public BlackboardVariable<GameObject> Beehive;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // If there is no Beehive game object, return a failure
        if (Beehive.Value == null) return Status.Failure;

        // Get the Hive script of the Hive object
        Hive hiveComponent = Beehive.Value.GetComponent<Hive>();

        // If Beehive game object doesn't have a "Hive" component
        if (hiveComponent == null) return Status.Failure;

        // Set the value of the nectar and pollen counts to the current count of the Hive
        HiveNectarCount.Value = hiveComponent.hiveNectarCount;
        HivePollenCount.Value = hiveComponent.hivePollenCount;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

