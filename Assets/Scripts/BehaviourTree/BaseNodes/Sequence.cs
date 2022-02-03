using System;

namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
   

    [CreateAssetMenu(fileName = "Sequence", menuName = "BehaviourTree/Nodes/Sequence")]
    public class Sequence : BTComposite
    {

        

        protected override NodeStates CancellableEvaluate()
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
        protected override NodeStates WaitForTheEndEvaluate()
        {
            for (int i = _currentChild; i < _nodes.Count; i++)
            {
                var child = _nodes[i];
                switch (child.Evaluate())
                {
                    case NodeStates.SUCCESS:
                        continue;
                    case NodeStates.FAILURE:
                        _currentChild = 0;
                        nodeState = NodeStates.FAILURE;
                        return nodeState;
                    case NodeStates.RUNNING:
                        _currentChild = i;
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
