using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{

    [CreateAssetMenu(fileName = "TargetInRange", menuName = "BehaviourTree/Nodes/Tasks/TargetInRange")]
    public class TargetInRange : BTTask
    {
        public override object Clone()
        {
            return CreateInstance<TargetInRange>();
        }

        public override NodeStates Evaluate()
        {
            if (Blackboard)
            {
                TankShooting shootComp = Blackboard.tankMovement.gameObject.GetComponent<TankShooting>();


                if (!Blackboard.closestEnemy)
                    return NodeStates.FAILURE;

                GameObject target = Blackboard.closestEnemy.gameObject;
                if (shootComp && shootComp.m_currCooldown <= 0 && target.GetComponent<TankHealth>() && shootComp.TargetCouldBeInRange())
                {
                    if (Vector3.Distance(target.transform.position, Blackboard.tankTransform.position) <= shootComp.m_shootDistance)
                    {
                        Debug.Log("Target In Range ?");
                        return NodeStates.SUCCESS;
                    }
                }
            }
            return NodeStates.FAILURE;
        }
    }
}
