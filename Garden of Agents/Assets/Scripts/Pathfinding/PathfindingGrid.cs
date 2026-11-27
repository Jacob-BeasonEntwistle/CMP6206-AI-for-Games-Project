using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class PathfindingGrid : MonoBehaviour {

	public LayerMask unwalkableMask;
	public LayerMask foodMask;
    public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

    public int maxScentLevel;

    void Awake() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

		// Value large enough to ensure the food can be sensed from opposite points in the grid
		maxScentLevel = 52;

		// Create the grid and generate the food scent gradient
        CreateGrid();
		GenerateFoodScent();
    }

	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));

				bool isFood = (Physics.CheckSphere(worldPoint, nodeRadius, foodMask));

				grid[x,y] = new Node(walkable,worldPoint, x,y, isFood);
			}
		}
	}

	// Generates a gradient to show the scent area of the food
	void GenerateFoodScent()
	{
		// For each node in the grid
		foreach (Node node in grid)
		{
			// Set default scent level to 0
			node.scentLevel = 0;

			// For each food node in the grid
			foreach (Node foodNode in grid)
			{
				// If its not a food source, ignore
				if (!foodNode.food) continue;

				// Get the distance between the food node and the current node
				int distance = Mathf.Abs(node.gridX - foodNode.gridX) + Mathf.Abs(node.gridY - foodNode.gridY);

				// The scent of the food is the max scent level minus the distance from the food
				int scent = maxScentLevel - distance;

				// If the scent is greater than the current node's scent level, set it to scent
				if (scent > node.scentLevel)
				{
					node.scentLevel = scent;
				}
			}
		}
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		// Subtracting transform.position.x/y accounts for the grid's offset
		float percentX = ((worldPosition.x - transform.position.x) + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = ((worldPosition.z - transform.position.z) + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	void OnDrawGizmos() {
		// If there is no grid, return
		if (grid == null) return;

		// For each node in the grid
		foreach (Node n in grid)
		{
			// Initialise the strongest scent
			int strongestScent = 0;
			int scent = 0;
			float scentPercent = 0;

			// For each node that is a food node in the grid
			foreach (Node foodNode in grid)
			{
				// If the node isn't a food node, ignore it
				if (!foodNode.food) continue;

				// Get the distance between the current node and the food source
				int distance = Mathf.Abs(n.gridX - foodNode.gridX) + Mathf.Abs(n.gridY - foodNode.gridY);

				// Return the greatest value out of the compared values (keeps result between 0 and 25)
				scent = Mathf.Max(0, maxScentLevel - distance);

				// If the scent is greater than the strongest scent, make the scent the new strongest
				if (scent > strongestScent)
				{
					strongestScent = scent;
				}

				// Calculate the percentage of the scent from 0.00 to 1.00
				scentPercent = strongestScent / (float)maxScentLevel;
			}

            // Change the colour of the node based on the percentage of the scent (should create a gradient)
            Gizmos.color = Color.Lerp(
                Color.white,
                Color.green,
                scentPercent
            );

			// If the node isn't walkable
            if (!n.walkable)
            {
				// Set the colour of the node to red
                Gizmos.color = Color.red;
            }

			// Set the transparency to 50%
			Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.50f);

            // Draw a cube for each node in the grid
            Gizmos.DrawCube(
                n.worldPosition,
                Vector3.one * (nodeDiameter - 0.1f)
            );
		}
	}

	public Node[,] GetGridArray()
	{
		return grid;
	}

	// Refresh the data within the grid to update the scent of the food
	public void RefreshFoodData()
	{
		// Synchronises the physics engine with any Transform changes made via manual position/rotation updates
		Physics.SyncTransforms();

		// For each node in the grid
		foreach (Node node in grid)
		{
			// If the node contains a food object
			bool isFood = (Physics.CheckSphere(node.worldPosition, nodeRadius, foodMask));

			// Set that node's food boolean to true
			node.food = isFood;
        }

		// Generate a new food scent gradient
		GenerateFoodScent();
	}
}