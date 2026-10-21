using System.Collections.Generic;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    // Create an array of worms
    [SerializeField] private GameObject wormPrefab;
    [SerializeField] private Transform wormPos;

    // Create a list to store the worm instances
    [SerializeField] private List<GameObject> worms = new();

    private float spawnTimer;
    private float wormSpawnRate = 10f;

    private void Start()
    {
        if (worms.Count == 0)
        {
            SpawnWorm();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // A constantly incrementing timer
        spawnTimer += Time.deltaTime;

        // If the timer reaches the spawn time for the worms
        if (spawnTimer >= wormSpawnRate)
        {
            SpawnWorm();

            if (worms.Count > 5)
            {
                worms.RemoveAt(0);
            }

            // Reset the timer
            spawnTimer = 0;
        }
    }

    private void SpawnWorm()
    {
        // Generate a random position for the worm to spawn
        wormPos.position = RandomWormSpawnpoint();

        // Spawn a worm at that random point
        GameObject wormInstance = Instantiate(wormPrefab, wormPos.position, Quaternion.identity);

        // Add the new instance to the list of worms
        worms.Add(wormInstance);

        // Apply a little up force
        wormInstance.GetComponent<Rigidbody>().AddForce(wormPos.up * 100f);
    }

    private Vector3 RandomWormSpawnpoint()
    {
        // Set the destination to a random point in a zone
        return new Vector3(
            Random.Range(-12.5f, 12.5f),
            0.5f,
            Random.Range(-8f, 19f)
        );
    }
}
