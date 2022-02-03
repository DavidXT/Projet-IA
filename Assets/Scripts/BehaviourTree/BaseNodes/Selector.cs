namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Selector", menuName = "BehaviourTree/Nodes/Selector")]
    public class Selector : BTComposite
    {

       

        protected override NodeStates CancellableEvaluate()
        {

            for (int i = 0; i < _nodes.Count; i++)
            {
                switch (_nodes[i].Evaluate())
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

        protected override NodeStates WaitForTheEndEvaluate()
        {
            for (int i = _currentChild; i < _nodes.Count; i++)
            {
                switch (_nodes[i].Evaluate())
                {
                    case NodeStates.FAILURE:
                        continue;
                    case NodeStates.SUCCESS:
                        _currentChild = 0;
                        nodeState = NodeStates.SUCCESS;
                        return nodeState;
                    case NodeStates.RUNNING:
                        _currentChild = i;
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
            foreach (BTNode node in _nodes)
            {
                selector._nodes.Add((BTNode)node.Clone());
            }
            return selector;
        }
    }
}
