namespace Complete
{
    using UnityEngine;

    public class LookAtTarget : BTNode
    {
        private TankMovement _tankMovement = null;
    
        public LookAtTarget(TankMovement tankMovement)
        {
            _tankMovement = tankMovement;
        }
        public override NodeStates Evaluate()
        {
            Blackboard blackboard = _tankMovement.BehaviourTree.Blackboard;
            Transform tankTransform = _tankMovement.transform;
            
            if (!Mathf.Approximately(Vector3.Dot(tankTransform.forward, (blackboard.targetLocation - tankTransform.position).normalized), 1))
            {
                tankTransform.RotateAround(tankTransform.position, tankTransform.up, Vector3.Angle(tankTransform.forward, (blackboard.targetLocation - tankTransform.position).normalized) * Time.deltaTime);
                return NodeStates.RUNNING;
            }

            return NodeStates.SUCCESS;
        }
    }
}