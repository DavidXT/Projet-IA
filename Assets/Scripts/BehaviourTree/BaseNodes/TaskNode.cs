public class TaskNode : BTNode
{
    public delegate NodeStates ActionNodeDelegate();

    private ActionNodeDelegate _action;

    public TaskNode (ActionNodeDelegate action)
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
