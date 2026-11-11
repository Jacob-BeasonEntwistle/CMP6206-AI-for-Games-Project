using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isAtHive", story: "[Bee] in [Hive] [HiveInteractArea]", category: "Conditions", id: "df16f19a65ccbca29881fe83468ea0f3")]
public partial class IsAtHiveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Hive;
    [SerializeReference] public BlackboardVariable<float> HiveInteractArea;

    public float interactiveArea = 4.0f;

    public override bool IsTrue()
    {
        if (Bee == null) return false;
        if (Hive == null) return false;
        if (HiveInteractArea == null) return false;
        if (Bee.Value == null) return false;
        if (Hive.Value == null) return false;

        float distance = Vector3.Distance(Bee.Value.transform.position, Hive.Value.transform.position);

        //Debug.Log(distance);
        //Debug.Log(distance < HiveInteractArea.Value);

        return distance < HiveInteractArea.Value;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
