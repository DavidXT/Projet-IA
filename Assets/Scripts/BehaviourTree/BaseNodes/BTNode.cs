using UnityEngine;

public abstract class BTNode : ScriptableObject
{
    public delegate NodeStates NodeReturn();

    protected NodeStates nodeState = NodeStates.NOTDEFINED;

    public NodeStates NodeState => nodeState;

    public abstract NodeStates Evaluate();
}
