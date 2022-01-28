using System.Collections.Generic;

public class Selector : BTNode
{
    private List<BTNode> _nodes = new List<BTNode>();

    public Selector(List<BTNode> nodes)
    {
        _nodes = nodes;
    }

    public override NodeStates Evaluate()
    {
        foreach (BTNode node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
                case NodeStates.RUNNING:
                    nodeState = NodeStates.RUNNING;
                    return nodeState;
                default:
                    continue;
            }
        }
        nodeState = NodeStates.FAILURE;
        return nodeState;
    }
}
