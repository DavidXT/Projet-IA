using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "BehaviourTree/Nodes/Sequence")]
public class Sequence : BTNode
{
    [SerializeField] private List<BTNode> _nodes = new List<BTNode>();

    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;
        foreach (BTNode node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
            }
        }
        nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return nodeState;
    }
}