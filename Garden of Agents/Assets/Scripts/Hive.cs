using JetBrains.Annotations;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public int hiveNectarCount;
    public int hivePollenCount;

    private int maxNectar = 100;
    private int maxPollen = 100;

    public bool nectarFull;
    public bool pollenFull;

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
    }

    public int GetNectarCount()
    {
        return hiveNectarCount;
    }
    public int GetPollenCount()
    {
        return hivePollenCount;
    }
}
