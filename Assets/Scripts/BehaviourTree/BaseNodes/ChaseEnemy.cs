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
            List<Vector3> path = tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position, Blackboard.targetTransform.position) ;

            if (path.Count <= 2) return NodeStates.FAILURE;

            if (!tankMovement.GetComponent<TankShooting>().TargetCouldBeInRange())
            {
                tankMovement.Rotate(path[1]);
                tankMovement.MovementInputValue = 1;
            }
            else
            {
                tankMovement.Rotate(Blackboard.targetTransform.position);
                tankMovement.MovementInputValue = 1;
            }

            if (Vector3.Distance(Blackboard.path[1], Blackboard.tankMovement.transform.position) > 0.2)
            {
                Blackboard.path.RemoveAt(1);
                return NodeStates.SUCCESS;
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
