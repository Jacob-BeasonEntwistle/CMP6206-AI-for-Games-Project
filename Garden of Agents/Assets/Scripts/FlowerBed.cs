using System.Threading;
using UnityEngine;

public enum FlowerType { Lavender, Sunflowers, Poppies };

public class FlowerBed : MonoBehaviour
{
    public int flowerNectarCount;
    public int flowerPollenCount;

    public int currentNectarCount;
    public int currentPollenCount;

    public FlowerType type;

    public float resourceTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setting the max nectar / pollen count
        if (type == FlowerType.Lavender)
        {
            flowerNectarCount = 100;
            flowerPollenCount = 100;
        }

        if (type == FlowerType.Sunflowers)
        {
            flowerNectarCount = 60;
            flowerPollenCount = 60;
        }

        if (type == FlowerType.Poppies)
        {
            flowerNectarCount = 30;
            flowerPollenCount = 30;
        }

        // Start the game with flower resources at half capacity
        currentNectarCount = flowerNectarCount / 2;
        currentPollenCount = flowerPollenCount / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Generate 1 nectar / pollen each second
        resourceTimer += Time.deltaTime;

        if (resourceTimer >= 1)
        {
            currentNectarCount += 1;
            currentPollenCount += 1;
            resourceTimer = 0;
        }

        // Clamp the nectar and pollen between their original max values and 0
        if (currentNectarCount >= flowerNectarCount)
        {
            currentNectarCount = flowerNectarCount;
        }
        if (currentNectarCount <= 0)
        {
            currentNectarCount = 0;
        }
        if (currentPollenCount >= flowerPollenCount)
        {
            currentPollenCount = flowerPollenCount;
        }
        if (currentPollenCount <= 0)
        {
            currentPollenCount = 0;
        }
    }

    public void CollectNectar()
    {
        currentNectarCount -= 20;
    }

    public void CollectPollen()
    {
        currentPollenCount -= 20;
    }
}
