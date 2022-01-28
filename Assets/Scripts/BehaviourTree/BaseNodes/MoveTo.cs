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
            Blackboard blackboard = _tankMovement.BehaviourTree.Blackboard;
            List<Vector3> path = _tankMovement.MovementMode.GetPathToLocation(blackboard.position, blackboard.targetLocation);

            if (path.Count == 0) return NodeStates.FAILURE;
            
            if (Vector3.Distance(blackboard.position, blackboard.targetLocation) > 20)
            {
                Debug.Log("Move");
                _tankMovement.transform.position += _tankMovement.transform.forward * blackboard.movementSpeed * Time.deltaTime;
                return NodeStates.RUNNING;
            }
            else
            {
                return NodeStates.SUCCESS;
            }
        }
    }
}
