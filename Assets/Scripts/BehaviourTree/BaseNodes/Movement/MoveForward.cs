namespace Complete
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "ChaseEnemy", menuName = "BehaviourTree/Nodes/Tasks/ChaseEnemy")]
    public class MoveForward : BTTask
    {
        public override NodeStates Evaluate()
        {
            if (Blackboard)
            {
                TankMovement tankMovement = Blackboard.tankMovement;
                List<Vector3> path = Blackboard.path;

                if (path.Count <= 2 || !tankMovement) return NodeStates.FAILURE;


                tankMovement.Rotate(path[1]);
                tankMovement.MovementInputValue = 1;
            

                if (Vector3.Distance(Blackboard.path[1], Blackboard.tankMovement.transform.position) > 0.2)
                {
                    Blackboard.path.RemoveAt(1);
                    return NodeStates.SUCCESS;
                }
                return NodeStates.RUNNING;
            }
            return NodeStates.FAILURE;
        }
        public override object Clone()
        {
            MoveForward chaseEnemy = CreateInstance<MoveForward>();
            return chaseEnemy;
        }
    }
}
