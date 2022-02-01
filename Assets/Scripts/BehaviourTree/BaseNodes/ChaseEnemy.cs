namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "ChaseEnemy", menuName = "BehaviourTree/Nodes/Tasks/ChaseEnemy")]
    public class ChaseEnemy : BTNode
    {
        private Blackboard Blackboard;
        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }

        public override NodeStates Evaluate()
        {
            Debug.Log("ChaseEnemy");
            TankMovement tankMovement = Blackboard.tankMovement;
            List<Vector3> path = tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position, Blackboard.targetLocation);

            if (path.Count <= 1) return NodeStates.FAILURE;

            if (!tankMovement.GetComponent<TankShooting>().TargetCouldBeInRange())
            {
                tankMovement.Rotate(path[1]);
                float input = Mathf.Sqrt(-2.7f * Mathf.Exp(-Mathf.Sqrt(Vector3.Distance(tankMovement.transform.position, Blackboard.targetLocation))) + 1);

                tankMovement.MovementInputValue = input;
            }
            else
            {
                tankMovement.Rotate(Blackboard.targetLocation);
                float input = Mathf.Sqrt(-2.7f * Mathf.Exp(-Mathf.Sqrt(Vector3.Distance(tankMovement.transform.position, Blackboard.targetLocation))) + 1);

                tankMovement.MovementInputValue = input;
            }
            return NodeStates.RUNNING;
        }
        public override object Clone()
        {
            ChaseEnemy chaseEnemy = CreateInstance<ChaseEnemy>();
            return chaseEnemy;
        }
    }
}
