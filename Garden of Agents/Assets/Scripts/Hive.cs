using System.Linq;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject[] bees;

    public int hiveNectarCount;
    public int hivePollenCount;

    private int maxNectar = 100;
    private int maxPollen = 100;

    public bool nectarFull;
    public bool pollenFull;

    private float resourceTimer;

    // On Start instantiate bee and assign to bees[]

    // Over time, add new bees and take a chunk of pollen

    // More bees = higher nectar consumption (bees.length)

    private void Update()
    {
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

        // Deplete resources
        resourceTimer += Time.deltaTime;

        if (resourceTimer >= 1)
        {
            if (hiveNectarCount <= 0)
            {
                hiveNectarCount = 0;
            }
            else
            {
                hiveNectarCount -= 1;
            }

            if (hivePollenCount <= 0)
            {
                hivePollenCount = 0;
            }
            else
            {
                hivePollenCount -= 2;
            }

            resourceTimer = 0;
        }
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
