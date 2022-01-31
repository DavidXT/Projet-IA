namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "GoToCaptureZone", menuName = "BehaviourTree/Nodes/Tasks/GoToCaptureZone")]
    public class GoToCaptureZone : BTNode
    {
        [SerializeField] private Blackboard Blackboard;
        
        public override NodeStates Evaluate()
        {
            TankMovement tankMovement = Blackboard.tankMovement;
            Transform tankTransform = tankMovement.transform;
            List<Vector3> path = tankMovement.MovementMode.GetPathToLocation(Blackboard.position, Blackboard.zoneLocation);

            Debug.Log("Go to capture zone");
            
            if (path.Count <= 1) return NodeStates.FAILURE;

            if (Vector3.Distance(tankTransform.position, Blackboard.zoneLocation) < 1f) return NodeStates.SUCCESS;

            tankMovement.Rotate(path[2]);
            tankMovement.MovementInputValue = 1;
            return NodeStates.RUNNING;
        }
    }
}