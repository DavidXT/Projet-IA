namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Selector", menuName = "BehaviourTree/Nodes/Selector")]
    public class Selector : BTNode
    {
        [SerializeField] private List<BTNode> _nodes = new List<BTNode>();

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
}
