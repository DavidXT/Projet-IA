using System;
using Complete;
using UnityEditor;
using UnityEngine;

public abstract class BTNode : ScriptableObject, ICloneable
{
    public delegate NodeStates NodeReturn();

    protected NodeStates nodeState = NodeStates.NOTDEFINED;

    public abstract void InitNode(Blackboard blackboard);
    public NodeStates NodeState => nodeState;

    public abstract NodeStates Evaluate();
    
    public abstract object Clone();
}
