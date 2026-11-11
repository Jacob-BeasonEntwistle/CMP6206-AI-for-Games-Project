using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isResourcesLow", story: "[hiveNectar] or [hivePollen] below [threshold]", category: "Conditions", id: "5df9385cb16742e960a427d2960972b0")]
public partial class IsResourcesLowCondition : Condition
{
    [SerializeReference] public BlackboardVariable<int> HiveNectar;
    [SerializeReference] public BlackboardVariable<int> HivePollen;
    [SerializeReference] public BlackboardVariable<int> Threshold;

    public override bool IsTrue()
    {
        //Debug.Log(HiveNectar.Value < Threshold.Value || HivePollen.Value < Threshold.Value);

        return HiveNectar.Value < Threshold.Value || HivePollen.Value < Threshold.Value;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
