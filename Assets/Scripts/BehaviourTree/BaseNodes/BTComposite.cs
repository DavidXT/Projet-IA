using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete
{


    public enum LoopMode
    {
        Cancellable,
        WaitForTheEnd
    }


    public abstract class BTComposite : BTNode
    {
        [SerializeField] protected List<BTNode> _nodes = new List<BTNode>();
        protected int _currentChild = 0;
        protected Blackboard _bb;
        [SerializeField] protected LoopMode _loopMode = LoopMode.Cancellable;

        protected abstract  NodeStates CancellableEvaluate();
        protected abstract NodeStates WaitForTheEndEvaluate();


        public override NodeStates Evaluate()
        {
            switch (_loopMode)
            {
                case LoopMode.Cancellable:
                    return CancellableEvaluate();
                case LoopMode.WaitForTheEnd:
                    return WaitForTheEndEvaluate();
                default:
                    return NodeStates.FAILURE;
            }

        }

        public override void InitNode(Blackboard blackboard)
        {
            foreach (var node in _nodes)
            {
                node.InitNode(blackboard);
            }

            _bb = blackboard;
        }
    }
}
