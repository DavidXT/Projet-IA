using UnityEngine.Serialization;

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
            Transform tankTransform = tankMovement.transform;
            
            if (!Mathf.Approximately(Vector3.Dot(tankTransform.forward, (Blackboard.targetLocation - tankTransform.position).normalized), 1))
            {
                tankTransform.RotateAround(tankTransform.position, tankTransform.up, Vector3.Angle(tankTransform.forward, (Blackboard.targetLocation - tankTransform.position).normalized) * Time.deltaTime);
                return NodeStates.RUNNING;
            }

            return NodeStates.SUCCESS;
        }

        public override object Clone()
        {
            LookAtTarget lookAtTarget = CreateInstance<LookAtTarget>();
            return lookAtTarget;
        }
    }
}