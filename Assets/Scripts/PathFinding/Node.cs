using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool m_walkable;
    public Vector3 m_worldPosition;
    public float DistanceWithEnd;
    public float DistanceFromStart;

    public Node(bool _walkable, Vector3 _worldpos)
    {
        m_walkable = _walkable;
        m_worldPosition = _worldpos;
    }

    public static bool Contains(List<Node> list, Node value)
    {
        foreach (Node tile in list)
        {
            if (tile == value)
            {
                return true;
            }
        }
        return false;
    }

    public static List<Node> Sort(List<Node> list)
    {
        for (int i = 0; i < list.Count - 1; ++i)
        {
            for (int j = 0; j < list.Count - 1; ++j)
            {
                Node tempTileA = list[j];
                Node tempTileB = list[j + 1];
                if (tempTileA.DistanceWithEnd > tempTileB.DistanceWithEnd)
                {
                    list[j] = tempTileB;
                    list[j + 1] = tempTileA;
                }
            }
        }
        return list;
    }
}
