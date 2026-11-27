using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class SnailFoodManager : MonoBehaviour
{
    public List<GameObject> snailFood;
    public GameObject snailFoodPrefab;
    public SnailAI snail;

    public bool foodEaten;
    public float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (snail == null)
        {
            snail = GameObject.Find("SnailAI").GetComponent<SnailAI>();
        }

        CreateFood();
    }

    // Update is called once per frame
    void Update()
    {
        // Removes all null snail food from the list
        snailFood.RemoveAll(s => s == null);

        // If the snail is eating, destroy the food object
        if (snail.GetIsEating())
        {
            if (!foodEaten)
            {
                Destroy(snailFood[0]);
                snailFood.RemoveAt(0);
                foodEaten = true;
            }
        }
        else if (!snail.GetIsEating() && snailFood.Count == 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= 10.0f)
            {
                spawnTimer = 0;

                if (snailFood.Count == 0)
                {
                    CreateFood();
                }
            }
        }
    }

    private void CreateFood()
    {
        GameObject cabbage = Instantiate(snailFoodPrefab);
        snailFood.Add(cabbage);
        foodEaten = false;
    }
}
