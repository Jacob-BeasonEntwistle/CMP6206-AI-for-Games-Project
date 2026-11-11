using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum FlowerType { Lavender, Sunflowers, Poppies };

public class FlowerBed : MonoBehaviour
{
    [Header("FlowerBed Resources")]
    public int currentNectarCount;
    public int currentPollenCount;

    public float resourceTimer;

    [Header("Flower Stats")]
    public FlowerType type;

    public int flowerNectarCount;
    public int flowerPollenCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setting the max nectar / pollen count
        if (type == FlowerType.Lavender)
        {
            flowerNectarCount = 100;
            flowerPollenCount = 80;
        }

        if (type == FlowerType.Sunflowers)
        {
            flowerNectarCount = 60;
            flowerPollenCount = 40;
        }

        if (type == FlowerType.Poppies)
        {
            flowerNectarCount = 30;
            flowerPollenCount = 10;
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

    public int GetNectar()
    {
        return currentNectarCount;
    }
    public int GetPollen()
    {
        return currentPollenCount;
    }
    public void TakeNectar(int nectar)
    {
        currentNectarCount -= nectar;

        if (currentNectarCount < 0)
        {
            currentNectarCount = 0;
        }
    }
    public void TakePollen(int pollen)
    {
        currentPollenCount -= pollen;

        if (currentPollenCount < 0)
        {
            currentPollenCount = 0;
        }
    }
}
