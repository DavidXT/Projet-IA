namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "GoToCaptureZone", menuName = "BehaviourTree/Nodes/Tasks/GoToCaptureZone")]
    public class GoToCaptureZone : BTNode
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
            List<Vector3> path = tankMovement.MovementMode.GetPathToLocation(Blackboard.position, Blackboard.zoneLocation);
            Blackboard.path = path.ToArray();

            if (path.Count <= 1) return NodeStates.FAILURE;

            if (Vector3.Distance(tankTransform.position, Blackboard.zoneLocation) < 1f) return NodeStates.SUCCESS;

            tankMovement.Rotate(path[0]);
            tankMovement.MovementInputValue = 1;
            
            return NodeStates.RUNNING;
        }
        
        public override object Clone()
        {
            GoToCaptureZone goToCaptureZone = CreateInstance<GoToCaptureZone>();
            return goToCaptureZone;
        }
    }
}