using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
	public Node Origin;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public static bool Contains(List<Node> list, Node value)
	{
		foreach (Node node in list)
		{
			if (node == value)
			{
				return true;
			}
		}
		return false;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}