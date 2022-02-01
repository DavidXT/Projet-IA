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
            Debug.Log("GoToCaptureZone");
            TankMovement tankMovement = Blackboard.tankMovement;
            Transform tankTransform = tankMovement.transform;
            
            
            if (Blackboard.path.Count == 0) return NodeStates.FAILURE;

            if (Vector3.Distance(tankTransform.position, Blackboard.targetLocation) < 1f) return NodeStates.SUCCESS;
            
            tankMovement.Rotate(Blackboard.path[0]);
            float input = Mathf.Sqrt(-2.7f * Mathf.Exp(-Mathf.Sqrt(Vector3.Distance(tankTransform.position, Blackboard.targetLocation))) + 1);
            //Debug.Log(input);
            tankMovement.MovementInputValue = input;
            if (Vector3.Distance(tankTransform.position, Blackboard.path[0]) < 1)
                Blackboard.path.RemoveAt(0);
            
            return NodeStates.RUNNING;
        }
        
        public override object Clone()
        {
            GoToCaptureZone goToCaptureZone = CreateInstance<GoToCaptureZone>();
            return goToCaptureZone;
        }
    }
}