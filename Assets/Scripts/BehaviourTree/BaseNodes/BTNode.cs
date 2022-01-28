using UnityEngine;

[System.Serializable]
public abstract class BTNode
{
    public delegate NodeStates NodeReturn();

    protected NodeStates nodeState = NodeStates.NOTDEFINED;

    public NodeStates NodeState => nodeState;
    
    public BTNode() {}

    public abstract NodeStates Evaluate();
}
