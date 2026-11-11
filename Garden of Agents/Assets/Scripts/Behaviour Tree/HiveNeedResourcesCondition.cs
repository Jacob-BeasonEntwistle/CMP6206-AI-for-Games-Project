using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "hiveNeedResources", story: "[needResources]", category: "Conditions", id: "be2df8320b10a9f5d7b9f3ed9fbba44e")]
public partial class HiveNeedResourcesCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> NeedResources;

    public override bool IsTrue()
    {
        return NeedResources;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
