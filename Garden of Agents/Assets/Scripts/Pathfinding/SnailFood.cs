using System.Collections.Generic;
using UnityEngine;

public class SnailFood : MonoBehaviour
{
    public PathfindingGrid grid;
    private Vector3 previousPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (grid == null)
        {
            grid = GameObject.FindWithTag("Grid").GetComponent<PathfindingGrid>();
        }

        SpawnAtRandomWalkableCell();
        grid.RefreshFoodData();

        // Set the starting/current node to the node it currently occupies
        Node currentNode = grid.NodeFromWorldPoint(transform.position);

        // Snap the agent to the centre of the current node
        transform.position = currentNode.worldPosition;

        previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != previousPos)
        {
            grid.RefreshFoodData();
            previousPos = transform.position;
        }
    }

    private void SpawnAtRandomWalkableCell()
    {
        // Create an empty list to store walkable nodes
        List<Node> walkableNodes = new List<Node>();

        // For each node in the grid's array
        foreach (Node n in grid.GetGridArray())
        {
            // If it's not walkable, ignore it
            if (!n.walkable) continue;

            // If it is walkable, store it in the list of walkable nodes
            walkableNodes.Add(n);
        }

        if (walkableNodes.Count > 0)
        {
            // Get a random number between 0 and the total number of walkable nodes
            int randomNode = Random.Range(0, walkableNodes.Count);

            Node selectedNode = walkableNodes[randomNode];
            transform.position = selectedNode.worldPosition;
        }
    }
}
