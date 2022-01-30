namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;

    public class MoveTo : BTNode
    {
        private TankMovement _tankMovement;
        
        public MoveTo(TankMovement tankMovement)
        {
            _tankMovement = tankMovement;
        }

        public override NodeStates Evaluate()
        {
            Transform tankTransform = _tankMovement.transform;
            Blackboard blackboard = _tankMovement.BehaviourTree.Blackboard;
            List<Vector3> path = _tankMovement.MovementMode.GetPathToLocation(blackboard.position, blackboard.targetLocation);

            if (path.Count == 0) return NodeStates.FAILURE;
            
            if (Vector3.Distance(blackboard.position, blackboard.targetLocation) > 5 || !_tankMovement.GetComponent<TankShooting>().TargetInRange())
            {
                if (!Mathf.Approximately(Vector3.Dot(path[0], (path[1] - path[0]).normalized), 1))
                {
                    tankTransform.RotateAround(tankTransform.position, tankTransform.up, Vector3.Angle(path[0], (path[1] - path[0]).normalized) * Time.deltaTime);
                    return NodeStates.RUNNING;
                }
                _tankMovement.transform.position += _tankMovement.transform.forward * blackboard.movementSpeed * Time.deltaTime;
                return NodeStates.RUNNING;
            }
            return NodeStates.SUCCESS;
        }
    }
}
