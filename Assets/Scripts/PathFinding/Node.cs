using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool m_walkable;
    public Vector3 m_worldPosition;

    public Node(bool _walkable, Vector3 _worldpos)
    {
        m_walkable = _walkable;
        m_worldPosition = _worldpos;
    }
}
