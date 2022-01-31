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
            TankMovement tankMovement = Blackboard.tankMovement;
            List<Vector3> path = tankMovement.MovementMode.GetPathToLocation(Blackboard.position, Blackboard.targetLocation);

            if (path.Count <= 1) return NodeStates.FAILURE;

            if (!tankMovement.GetComponent<TankShooting>().TargetCouldBeInRange())
            {
                tankMovement.Rotate(path[0]);
                tankMovement.MovementInputValue = 1;
            }
            else
            {
                tankMovement.Rotate(Blackboard.targetLocation);
                tankMovement.MovementInputValue = 1;
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
