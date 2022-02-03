using System.Linq.Expressions;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "LookAtTarget", menuName = "BehaviourTree/Nodes/Tasks/LookAtTarget")]
    public class LookAtTarget : BTNode
    {
        private Blackboard Blackboard;

        int loop = 0;

        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            loop++;
            if (loop > 100)
            {
                loop = 0;
                return NodeStates.FAILURE;

            }
            TankMovement tankMovement = Blackboard.tankMovement;
            Transform tankTransform = Blackboard.tankTransform;

            if (Blackboard.path.Count <= 1) return NodeStates.FAILURE;

            tankMovement.Rotate(Blackboard.path[1]);
            if (Vector3.Dot(tankTransform.forward, (Blackboard.path[1] - tankTransform.position).normalized) <= 0.9f)
            {
                return NodeStates.RUNNING;
            }
            else
            {
                loop = 0;
                tankMovement.TurnInputValue = 0;
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