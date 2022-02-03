namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Selector", menuName = "BehaviourTree/Nodes/Selector")]
    public class Selector : BTNode
    {
        [SerializeField] private List<BTNode> _nodes = new List<BTNode>();

        public override void InitNode(Blackboard blackboard)
        {
            foreach (var node in _nodes)
            {
                node.InitNode(blackboard);
            }
        }

        public override NodeStates Evaluate()
        {
            foreach (BTNode node in _nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        Debug.Log("Selector fail");
                        continue;
                    case NodeStates.SUCCESS:
                        Debug.Log("Selector success");
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

        public override object Clone()
        {
            Selector selector = CreateInstance<Selector>();
            foreach (var node in _nodes)
            {
                selector._nodes.Add((BTNode)node.Clone());
            }
            return selector;
        }
    }
}
