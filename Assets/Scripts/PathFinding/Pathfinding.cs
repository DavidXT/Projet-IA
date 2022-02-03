using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Complete;

public class Pathfinding : MonoBehaviour
{
	public static Pathfinding Instance;
	Grid grid;
	public bool b_AStar = true;

	void Awake()
	{
		if(Instance == null)
        {
			Instance = this;
        }
		grid = GetComponent<Grid>();
	}

	void Update()
	{
	}

	public void AStar(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0)
		{
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
				{
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode)
			{
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	public void Dijkstra(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);
		List<Node> NodeToCheck = new List<Node>();
		List<Node> NodeChecked = new List<Node>();
		Node[,] tempsGrid = Grid.Instance.grid;

		Node tempsEndNode = null;
		NodeToCheck.Add(startNode);

		while (NodeToCheck.Count > 0)
		{
			Node node = NodeToCheck[0];
			NodeToCheck.RemoveAt(0);

			//Haut
			if (node.worldPosition.z < Grid.Instance.gridSizeY - 1)
			{
				Node newTempNode = tempsGrid[node.gridX, node.gridY + 1];
                if (newTempNode.walkable)
                {
					if (!Node.Contains(NodeToCheck, newTempNode) && !Node.Contains(NodeChecked, newTempNode))
					{
						NodeToCheck.Add(newTempNode);
						newTempNode.parent = node;
						if (newTempNode == targetNode)
						{
							tempsEndNode = newTempNode;
							RetracePath(startNode, tempsEndNode);
							return;
						}
					}
				}

			}


			////droite
			if (node.worldPosition.x < Grid.Instance.gridSizeX - 1)
			{
				Node newTempNode = tempsGrid[node.gridX + 1, node.gridY];
				if (newTempNode.walkable)
                {
					if (!Node.Contains(NodeToCheck, newTempNode) && !Node.Contains(NodeChecked, newTempNode))
					{
						NodeToCheck.Add(newTempNode);
						newTempNode.parent = node;
						if (newTempNode == targetNode)
						{
							tempsEndNode = newTempNode;
							RetracePath(startNode, tempsEndNode);
							return;
						}
					}
				}

			}


			////Bas
			if (node.worldPosition.z > 0)
			{
				Node newTempNode = tempsGrid[node.gridX, node.gridY - 1];
				if (newTempNode.walkable)
                {
					if (!Node.Contains(NodeToCheck, newTempNode) && !Node.Contains(NodeChecked, newTempNode))
					{
						NodeToCheck.Add(newTempNode);
						newTempNode.parent = node;
						if (newTempNode == targetNode)
						{
							tempsEndNode = newTempNode;
							RetracePath(startNode, tempsEndNode);
							return;
						}
					}
				}

			}


			////gauche
			if (node.worldPosition.x > 0)
			{
				Node newTempNode = tempsGrid[node.gridX - 1, node.gridY];
                if (newTempNode.walkable)
                {
					if (!Node.Contains(NodeToCheck, newTempNode) && !Node.Contains(NodeChecked, newTempNode))
					{
						NodeToCheck.Add(newTempNode);
						newTempNode.parent = node;
						if (newTempNode == targetNode)
						{
							tempsEndNode = newTempNode;
							RetracePath(startNode, tempsEndNode);
							return;
						}
					}
				}
			}

			NodeChecked.Add(node);
		}
    }

    void RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		grid.path = path;
	}


	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}