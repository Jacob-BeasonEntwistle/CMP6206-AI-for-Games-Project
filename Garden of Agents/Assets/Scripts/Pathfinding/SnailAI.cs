using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnailAI : MonoBehaviour
{
    public PathfindingGrid grid;
    public Node currentNode;
    public Node previousNode;

    public float moveTimer;

    public SnailFoodManager snailFoodManager;

    public bool isEating;
    public float eatingTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (grid == null)
        {
            grid = GameObject.FindWithTag("Grid").GetComponent<PathfindingGrid>();
        }

        // Set the starting/current node to the node it currently occupies
        currentNode = grid.NodeFromWorldPoint(transform.position);

        // Snap the agent to the centre of the current node
        transform.position = currentNode.worldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode.food)
        {
            isEating = true;
            grid.RefreshFoodData();
        }

        if (!isEating)
        {
            moveTimer += Time.deltaTime;

            // If the timer is greater than or equal to 2.0f
            if (moveTimer >= 2.0f)
            {
                // Find the next node to move to
                FindStrongestScent();
                // Reset timer
                moveTimer = 0;
            }
        }
        else
        {
            eatingTimer += Time.deltaTime;

            if (eatingTimer >= 10.0f)
            {
                isEating = false;
                eatingTimer = 0;
            }
        }
    }

    // Finds the neighbouring node with the strongest scent
    private void FindStrongestScent()
    {
        // Set the next node to none
        Node nextNode = null;

        // Get the list of neighbours of the current node
        List<Node> neighbours = grid.GetNeighbours(currentNode);

        // Initiate the strongest scent as the current node's scent
        int strongestScent = currentNode.scentLevel;

        // Set the next node to the current node by default (in case there are no valid neighbours to move to)
        nextNode = currentNode;

        // For each node in the list of neighbours
        foreach (Node node in neighbours)
        {
            // If the node is unwalkable, ignore it
            if (!node.walkable) continue;

            // If the node is the previously visited node, ignore it
            if (node == previousNode) continue;

            // If the scent level is greater than the current strongest scent
            if (node.scentLevel > strongestScent)
            {
                // Set the strongest scent to that nodes scent level
                strongestScent = node.scentLevel;
                // Make the next node to move to, the node with the strongest scent level
                nextNode = node;
            }
        }

        // Make the previous node the current node before moving
        previousNode = currentNode;
        // Call the function that moves the agent to the next node
        MoveToNextNode(nextNode);
    }

    // Moves the agent to the next node
    private void MoveToNextNode(Node nextNode)
    {
        // Get the position of the next node and work out the rotation needed to face it
        Quaternion rotation = Quaternion.LookRotation(nextNode.worldPosition - transform.position, Vector3.up);
        // Apply the rotation to the agents current rotation
        transform.rotation = rotation;
        // Set the position of the agent to the next node's position
        transform.position = nextNode.worldPosition;
        // Set the current node to the next node in order to reset the movement process
        currentNode = nextNode;
    }

    public bool GetIsEating()
    {
        return isEating;
    }
}
