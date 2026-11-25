using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;
	public bool food;
	public int foodScentStrength;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, bool _food) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		food = _food;

		if (food)
		{
			foodScentStrength = 25;
		}
		else
		{
			foodScentStrength = 0;
		}
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
