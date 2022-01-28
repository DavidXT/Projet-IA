using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public abstract class BTNode : ScriptableObject
{
    public delegate NodeStates NodeReturn();

    protected NodeStates nodeState = NodeStates.NOTDEFINED;

    public NodeStates NodeState => nodeState;
    
    public BTNode() {}

    public abstract NodeStates Evaluate();
}
