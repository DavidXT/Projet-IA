using System.Linq.Expressions;

namespace Complete
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "LookAtTarget", menuName = "BehaviourTree/Nodes/Tasks/LookAtTarget")]
    public class LookAtTarget : BTTask
    {
        int loop = 0;

        public override NodeStates Evaluate()
        {
            loop++;
            if (loop > 100)
            {
                loop = 0;
                return NodeStates.FAILURE;

            }
            if (Blackboard)
            {
                TankMovement tankMovement = Blackboard.tankMovement;
                Transform tankTransform = Blackboard.tankTransform;
                Transform target = Blackboard.targetTransform;

                if (tankMovement && tankTransform && target && target.gameObject.GetComponent<TankHealth>())
                {
                    tankMovement.Rotate(target.position );
                    if (Vector3.Dot(tankTransform.forward, (target.position - tankTransform.position).normalized) <= Blackboard.acceptance)
                    {
                        return NodeStates.RUNNING;
                    }
                    else
                    {
                        Debug.Log("I See You");
                        loop = 0;
                        tankMovement.TurnInputValue = 0;
                        return NodeStates.SUCCESS;
                    }

                }
            }

            return NodeStates.FAILURE;          
        }

        public override object Clone()
        {
            LookAtTarget lookAtTarget = CreateInstance<LookAtTarget>();
            return lookAtTarget;
        }
    }
}