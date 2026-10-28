using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isAtHive", story: "[Bee] at [Hive]", category: "Conditions", id: "df16f19a65ccbca29881fe83468ea0f3")]
public partial class IsAtHiveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Hive;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
