using System.Runtime.InteropServices.WindowsRuntime;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Inverter", menuName = "BehaviourTree/Nodes/Inverter")]
    public class Inverter : BTNode
    {
        [SerializeField] private BTNode _node = null;
        
        public override void InitNode(Blackboard blackboard)
        {
            _node.InitNode(blackboard);
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
        
        public override object Clone()
        {
            Inverter inverter = CreateInstance<Inverter>();
            inverter._node = (BTNode)_node.Clone();
            return inverter;
        }
    }
}
