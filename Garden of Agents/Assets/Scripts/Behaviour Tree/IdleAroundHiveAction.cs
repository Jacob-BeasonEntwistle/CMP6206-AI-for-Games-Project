using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "IdleAroundHive", story: "[Bee] flies around [Hive] in [hiveInteractArea] at [Speed]", category: "Action", id: "520429c06d71dc63c34630c5ebae4236")]
public partial class IdleAroundHiveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Bee;
    [SerializeReference] public BlackboardVariable<GameObject> Hive;
    [SerializeReference] public BlackboardVariable<float> HiveInteractArea;
    [SerializeReference] public BlackboardVariable<float> Speed;
    private Vector3 point;
    private float distance;

    protected override Status OnStart()
    {
        distance = 0f;

        point = PickRandomPoint();

        return Status.Running;
    }

    private Vector3 PickRandomPoint()
    {
        Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * (HiveInteractArea.Value);
        return Hive.Value.transform.position + new Vector3(randomPoint.x, 0, randomPoint.z);
    }

    protected override Status OnUpdate()
    {
        distance = Vector3.Distance(Bee.Value.transform.position, point);

        if (distance <= 1.0f)
        {
            point = PickRandomPoint();
            point.y = 1.75f;
        }

        Bee.Value.transform.position = Vector3.MoveTowards(
            Bee.Value.transform.position,
            point,
            Speed.Value * Time.deltaTime
        );

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

