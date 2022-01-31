using System;

namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Sequence", menuName = "BehaviourTree/Nodes/Sequence")]
    public class Sequence : BTNode, ICloneable
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

        public override object Clone()
        {
            Sequence seq = CreateInstance<Sequence>();
            foreach (var node in _nodes)
            {
                seq._nodes.Add((BTNode)node.Clone());
            }
            return seq;
        }
    }
}
