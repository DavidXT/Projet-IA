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


            for (int i = 0; i < _nodes.Count; i++)
            {
                var child = _nodes[i];
                switch (child.Evaluate())
                {
                    case NodeStates.SUCCESS:
                        continue;
                    case NodeStates.FAILURE:
                        nodeState = NodeStates.FAILURE;
                        return nodeState;
                    case NodeStates.RUNNING:
                        nodeState = NodeStates.RUNNING;
                        return nodeState;
                }
            }
            nodeState = NodeStates.SUCCESS;
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
