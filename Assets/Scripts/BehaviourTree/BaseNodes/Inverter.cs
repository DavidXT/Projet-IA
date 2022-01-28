using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inverter", menuName = "BehaviourTree/Nodes/Inverter")]
public class Inverter : BTNode
{
    [SerializeField] private BTNode _node = null;

    public override NodeStates Evaluate()
    {
        switch (_node.Evaluate())
        {
            case NodeStates.FAILURE:
                nodeState = NodeStates.SUCCESS;
                return nodeState;
            case NodeStates.SUCCESS:
                nodeState = NodeStates.FAILURE;
                return nodeState;
            case NodeStates.RUNNING:
                nodeState = NodeStates.RUNNING;
                return nodeState;
            default:
                nodeState = NodeStates.SUCCESS;
                return nodeState;
        }
    }
}