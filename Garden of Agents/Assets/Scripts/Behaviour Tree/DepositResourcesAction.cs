using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DepositResources", story: "Deposit [beeNectar] and [beePollen] to [Hive]", category: "Action", id: "48a8258c72db0e0bcfd322946aae69ad")]
public partial class DepositResourcesAction : Action
{
    [SerializeReference] public BlackboardVariable<int> BeeNectar;
    [SerializeReference] public BlackboardVariable<int> BeePollen;
    [SerializeReference] public BlackboardVariable<GameObject> Hive;

    protected override Status OnStart()
    {
        Hive.Value.GetComponent<Hive>().DepositResources(BeeNectar, BeePollen);
        BeeNectar.Value = 0;
        BeePollen.Value = 0;

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

