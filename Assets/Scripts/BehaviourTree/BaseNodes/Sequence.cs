using System.Collections.Generic;

public class Sequence : BTNode
{
    private List<BTNode> _nodes = new List<BTNode>();

    public Sequence(List<BTNode> nodes)
    {
        _nodes = nodes;
    }

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