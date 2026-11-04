using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isInventoryFull", story: "Is [inventoryFull]: [beeNectar] + [beePollen] >= [maxResourceCapacity]", category: "Conditions", id: "7441c79d9435dd370b99ed7fb9f3d5ef")]
public partial class IsInventoryFullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> InventoryFull;
    [SerializeReference] public BlackboardVariable<int> BeeNectar;
    [SerializeReference] public BlackboardVariable<int> BeePollen;
    [SerializeReference] public BlackboardVariable<int> MaxResourceCapacity;

    public override bool IsTrue()
    {
        int sumOfResources = BeeNectar.Value + BeePollen.Value;
        if (sumOfResources >= 0 && sumOfResources < MaxResourceCapacity.Value)
        {
            return false;
        }

        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
