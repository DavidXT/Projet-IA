using System.Linq.Expressions;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "LookAtTarget", menuName = "BehaviourTree/Nodes/Tasks/LookAtTarget")]
    public class LookAtTarget : BTNode
    {
        private Blackboard Blackboard;

        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            
            TankMovement tankMovement = Blackboard.tankMovement;
            Transform tankTransform = Blackboard.tankTransform;
            
            tankMovement.Rotate(Blackboard.path[0]);
            if (Vector3.Dot(tankTransform.forward, (Blackboard.path[0] - tankTransform.position).normalized) <= 0.8f)
            {
                Debug.Log("LookAtTarget");
                Debug.Log(Vector3.Dot(tankTransform.position, (Blackboard.path[1] - tankTransform.position).normalized));
                return NodeStates.RUNNING;
            }
            else
            {
                return NodeStates.SUCCESS;
            }
        }

        public override object Clone()
        {
            LookAtTarget lookAtTarget = CreateInstance<LookAtTarget>();
            return lookAtTarget;
        }
    }
}