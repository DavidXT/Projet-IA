namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "MoveToTarget", menuName = "BehaviourTree/Nodes/Tasks/MoveToTarget")]
    public class MoveToTarget : BTNode
    {
        private Blackboard Blackboard;
        int Loop = 0;
        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            Loop++;
            TankMovement tankMovement = this.Blackboard.tankMovement;
            Transform tankTransform = tankMovement.transform;

            Vector3 tempPos = Blackboard.path[1];
            if (Blackboard.path.Count == 0 || Vector3.Dot(tankTransform.forward, (tempPos - tankTransform.position).normalized) <= 0.5f || Loop > 100)
            {
                tankMovement.MovementInputValue = 0;
                Loop = 0;
                return NodeStates.FAILURE;

            }

            float input = Mathf.Sqrt(-2.7f * Mathf.Exp(-Mathf.Sqrt(Vector3.Distance(tankTransform.position, Blackboard.targetTransform.position))) + 1);
            tankMovement.MovementInputValue = input;

            if (Vector3.Distance(tankTransform.position, tempPos) < 2 || Blackboard.path.Count <= 2)
            {
                Blackboard.path.RemoveAt(0);
                Loop = 0;
                return NodeStates.SUCCESS;
            }


            return NodeStates.RUNNING;
        }
        
        public override object Clone()
        {
            MoveToTarget goToCaptureZone = CreateInstance<MoveToTarget>();
            return goToCaptureZone;
        }
    }
}