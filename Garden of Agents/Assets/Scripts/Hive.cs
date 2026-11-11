using System.Linq;
using UnityEngine;

public class Hive : MonoBehaviour
{
    [Header("Hive Resources")]
    public int hiveNectarCount;         // Hive current nectar
    public int hivePollenCount;         // Hive current pollen

    private int maxNectar = 100;
    private int maxPollen = 100;

    public bool nectarFull;
    public bool pollenFull;

    private float resourceTimer;

    [Header("Object References")]
    public GameObject beePrefab;
    public GameObject[] bees;

    [Header("Bee Production")]
    public int maxBees = 10;            // Maxium bees allowed
    private int pollenReq = 20;         // Pollen requirement
    private float beeProductionTimer;

    private void Start()
    {
        // On Start instantiate bee and assign to bees[]
        GameObject beeInstance = Instantiate(beePrefab, transform.position, Quaternion.identity);
        bees = GameObject.FindGameObjectsWithTag("Bee");
    }

    private void Update()
    {
        // If the hive nectar or pollen reach maximum capacity, the resource is full
        if (hiveNectarCount > maxNectar)
        {
            hiveNectarCount = maxNectar;
            nectarFull = true;
        }
        if (hivePollenCount > maxPollen)
        {
            hivePollenCount = maxPollen;
            pollenFull = true;
        }

        // Resource depletion
        resourceTimer += Time.deltaTime;

        // Every second, deplete resources
        if (resourceTimer >= 1)
        {
            // If the nectar count is already at 0, don't reduce
            if (hiveNectarCount <= 0)
            {
                hiveNectarCount = 0;
            }
            else
            {
                // Decrease nectar based on how many bees active
                // More bees = Higher Nectar Consumption (bees.length)
                hiveNectarCount -= bees.Length;
            }

            // If the pollen count is already at 0, don't reduce
            if (hivePollenCount <= 0)
            {
                hivePollenCount = 0;
            }
            else
            {
                // Decrease pollen count
                hivePollenCount -= 1;
            }

            // Reset timer
            resourceTimer = 0;
        }

        // Bee production
        // If the hive has enough pollen to produce bees
        if (hivePollenCount >= pollenReq)
        {
            // Start the production timer
            beeProductionTimer += Time.deltaTime;

            // If the hive still has enough pollen after 30s, spawn a bee
            if (beeProductionTimer >= 5)
            {
                // Hive max bee capacity prevents more bees spawning
                if (bees.Length <= maxBees)
                {
                    CreateBee();
                }

                // Reset timer
                beeProductionTimer = 0;
            }
        }        
    }

    public void CreateBee()
    {
        // Create a new instance of a bee
        GameObject beeInstance = Instantiate(beePrefab, transform.position, Quaternion.identity);
        // Take a chunk of pollen
        hivePollenCount -= pollenReq;
        // Update the bees array so that all bees are assigned to the hive
        bees = GameObject.FindGameObjectsWithTag("Bee");
    }

    public int GetNectarCount()
    {
        return hiveNectarCount;
    }
    public int GetPollenCount()
    {
        return hivePollenCount;
    }

    public void DepositResources(int nectar, int pollen)
    {
        Debug.Log($"{nectar} nectar and {pollen} pollen deposited");

        hiveNectarCount += nectar;
        hivePollenCount += pollen;
    }
}
