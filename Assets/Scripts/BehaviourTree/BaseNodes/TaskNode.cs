using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "BehaviourTree/Nodes/Task")]
public class TaskNode : BTNode
{
    public delegate NodeStates ActionNodeDelegate();

    private ActionNodeDelegate _action = () =>
    {
        Debug.Log("Nodes passed + 1");
        return NodeStates.SUCCESS;
    };

    public void SetupAction(ActionNodeDelegate action)
    {
        _action = action;
    }
    
    public override NodeStates Evaluate()
    {
        switch (_action())
        {
            case NodeStates.SUCCESS:
                nodeState = NodeStates.SUCCESS;
                return nodeState;
            case NodeStates.FAILURE:
                nodeState = NodeStates.FAILURE;
                return nodeState;
            case NodeStates.RUNNING:
                nodeState = NodeStates.RUNNING;
                return nodeState;
            default:
                nodeState = NodeStates.FAILURE;
                return nodeState;
        }
    }
}
