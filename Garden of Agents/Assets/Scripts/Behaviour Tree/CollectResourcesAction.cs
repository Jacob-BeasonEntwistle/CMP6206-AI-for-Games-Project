using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CollectResources", story: "Collect resources from [Flower] and store in [beeNectar] and [beePollen] until [inventoryFull] [maxResourceCapacity] reached", category: "Action", id: "6df45b49938ac76d594c663b29e78928")]
public partial class CollectResourcesAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Flower;
    [SerializeReference] public BlackboardVariable<int> BeeNectar;
    [SerializeReference] public BlackboardVariable<int> BeePollen;
    [SerializeReference] public BlackboardVariable<bool> InventoryFull;
    [SerializeReference] public BlackboardVariable<int> MaxResourceCapacity;

    private FlowerBed flower;

    private int currentResources;
    private int availableSpace;

    private float gatherTimer;
    private int gatherRate;

    protected override Status OnStart()
    {
        // Get the reference to the FlowerBed script on the selected flower
        flower = Flower.Value.GetComponent<FlowerBed>();

        // 10 units of nectar/pollen
        gatherRate = 10;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Check how many resources bee is carrying
        currentResources = BeeNectar.Value + BeePollen.Value;
        // Check how much more the bee can collect
        availableSpace = MaxResourceCapacity.Value - currentResources;

        // If the bee has a full inventory, action complete
        if (BeeNectar.Value + BeePollen.Value >= MaxResourceCapacity.Value)
        {
            InventoryFull.Value = true;
            return Status.Success;
        }

        // Check if the flower has any nectar or pollen remaining
        if (flower.GetNectar() <= 0.0001 && flower.GetPollen() <= 0.0001)
        {
            return Status.Failure;
        }

        // Resource Collection
        gatherTimer += Time.deltaTime;

        // Every second
        if (gatherTimer >= 1)
        {
            // If there is nectar
            if (flower.GetNectar() > 0)
            {
                // Collect some nectar from the flower
                // If the flower has less nectar than the amount it tried to collect, take the rest
                int nectarToCollect = Mathf.Min(gatherRate, flower.GetNectar(), availableSpace);

                // Add nectar to the bees inventory
                BeeNectar.Value += nectarToCollect;
                // Remove nectar from the flower
                flower.TakeNectar(nectarToCollect);
            }

            // Recalculate available space
            availableSpace = MaxResourceCapacity.Value - (BeeNectar.Value + BeePollen.Value);

            // If there is pollen
            if (flower.GetPollen() > 0)
            {
                // Collect some pollen from the flower
                // If the flower has less pollen than the amount it tried to collect, take the rest
                int pollenToCollect = Mathf.Min(gatherRate, flower.GetPollen(), availableSpace);

                // Add pollen to the bees inventory
                BeePollen.Value += pollenToCollect;
                // Remove pollen from the flower
                flower.TakePollen(pollenToCollect);
            }

            gatherTimer = 0;
        }

        // Recalculate resources in inventory
        currentResources = BeeNectar.Value + BeePollen.Value;

        // If bee's inventory is full, action complete
        if (currentResources >= MaxResourceCapacity.Value)
        {
            InventoryFull.Value = true;
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

