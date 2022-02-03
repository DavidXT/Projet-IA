using System;

namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Sequence", menuName = "BehaviourTree/Nodes/Sequence")]
    public class Sequence : BTNode, ICloneable
    {
        [SerializeField] private List<BTNode> _nodes = new List<BTNode>();
        private int currentChild = 0;
        private Blackboard bb;
        public override void InitNode(Blackboard blackboard)
        {
            foreach (var node in _nodes)
            {
                node.InitNode(blackboard);
            }

            bb = blackboard;
        }

        public override NodeStates Evaluate()
        {
            if (_nodes.Count > 0)
            {
                if (_nodes.Count > currentChild)
                {
                    switch (_nodes[currentChild].Evaluate())
                    {
                        case NodeStates.FAILURE:
                            currentChild = 0;
                            nodeState = NodeStates.FAILURE;
                            return nodeState;
                        case NodeStates.SUCCESS:
                            currentChild++;
                            Evaluate();
                            break;
                        case NodeStates.RUNNING:
                            nodeState = NodeStates.RUNNING;
                            return nodeState;
                    }
                }
                else
                {
                    nodeState = NodeStates.SUCCESS;
                    currentChild = 0;
                    return nodeState;
                }
            }
            nodeState = NodeStates.FAILURE;
            return NodeState;
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
