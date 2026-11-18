using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum FlowerType { None, Lavender, Sunflowers, Poppies };

public class FlowerBed : MonoBehaviour
{
    [Header("Flower Meshes")]
    [SerializeField] private GameObject lavenderPrefab;
    [SerializeField] private GameObject sunflowerPrefab;
    [SerializeField] private GameObject poppiesPrefab;

    private Transform flowerTransform;
    private Transform collectionPoint;

    [Header("Flower Stats")]
    public FlowerType type;

    public int flowerNectarCount;
    public int flowerPollenCount;

    [Header("FlowerBed Resources")]
    public int currentNectarCount;
    public int currentPollenCount;

    public float resourceTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (flowerTransform == null)
        {
            flowerTransform = transform.Find("Flowers");
        }

        // Setting the max nectar / pollen count and changing the appearance of the flowers
        switch (type)
        {
            case FlowerType.Lavender:
                flowerNectarCount = 100;
                flowerPollenCount = 80;

                Instantiate(lavenderPrefab, flowerTransform);
                break;
            case FlowerType.Sunflowers:
                flowerNectarCount = 60;
                flowerPollenCount = 40;

                Instantiate(sunflowerPrefab, flowerTransform);
                break;
            case FlowerType.Poppies:
                flowerNectarCount = 30;
                flowerPollenCount = 10;

                Instantiate(poppiesPrefab, flowerTransform);
                break;
        }

        // Start the game with flower resources at max capacity
        currentNectarCount = flowerNectarCount;
        currentPollenCount = flowerPollenCount;
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
    public Transform GetFlowerCollectionPoint()
    {
        // First GetChild(0): "Flowers"
        // Second GetChild(0): "Lavender/Sunflowers/Poppies"
        return transform.GetChild(0).GetChild(0).Find("CollectionPoint"); ;
    }
}
