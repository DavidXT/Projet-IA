public class Inverter : BTNode
{
    private BTNode _node = null;

    public Inverter(BTNode node)
    {
        _node = node;
    }

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