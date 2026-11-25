using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;			// States whether its a node that the agent can walk on
	public bool food;				// States whether its a node containing a food source
	public int scentLevel;			// The strength of the scent of a node
	public Vector3 worldPosition;	// The node's world position
	public int gridX;				// The node's x coordinate in the grid
	public int gridY;               // The node's y coordinate in the grid

	public int gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, bool _food) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		food = _food;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
