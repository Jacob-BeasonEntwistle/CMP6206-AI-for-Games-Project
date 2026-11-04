using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CollectResources", story: "Collect resources from [Flower] and store in [beeNectar] and [beePollen]", category: "Action", id: "6df45b49938ac76d594c663b29e78928")]
public partial class CollectResourcesAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Flower;
    [SerializeReference] public BlackboardVariable<int> BeeNectar;
    [SerializeReference] public BlackboardVariable<int> BeePollen;
    protected override Status OnStart()
    {
        Flower.Value.GetComponent<FlowerBed>().CollectNectar();
        BeeNectar.Value += 20;
        Flower.Value.GetComponent<FlowerBed>().CollectPollen();
        BeePollen.Value += 20;

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

