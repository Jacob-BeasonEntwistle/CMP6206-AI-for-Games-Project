using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "hasResources", story: "Holding [Nectar] or [Pollen]", category: "Conditions", id: "7e735828ca5a91590ccaaccf06217ebd")]
public partial class HasResourcesCondition : Condition
{
    [SerializeReference] public BlackboardVariable<int> Nectar;
    [SerializeReference] public BlackboardVariable<int> Pollen;

    public override bool IsTrue()
    {
        return Nectar.Value > 0 || Pollen.Value > 0;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
