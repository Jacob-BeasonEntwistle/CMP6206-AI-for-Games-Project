using UnityEngine;

public class Hive : MonoBehaviour
{
    public int hiveNectarCount;
    public int hivePollenCount;

    private int maxNectar = 100;
    private int maxPollen = 100;

    public bool nectarFull;
    public bool pollenFull;

    private float resourceTimer;

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

        //// Deplete resources
        //resourceTimer += Time.deltaTime;

        //if (resourceTimer >= 1)
        //{
        //    hiveNectarCount -= 1;
        //    hivePollenCount -= 2;
        //    resourceTimer = 0;
        //}
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
