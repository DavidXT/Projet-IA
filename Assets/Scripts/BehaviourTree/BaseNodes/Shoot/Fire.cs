using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    [CreateAssetMenu(fileName = "Fire", menuName = "BehaviourTree/Nodes/Tasks/Fire")]

    public class Fire : BTTask
    {
        public override object Clone()
        {
            return CreateInstance<Fire>();
        }

        public override NodeStates Evaluate()
        {
            if (Blackboard)
            {
                TankShooting shootComp = Blackboard.tankMovement.gameObject.GetComponent<TankShooting>();
                if (shootComp)
                {
                    Debug.Log("Fire");
                    shootComp.Fire();
                    return NodeStates.SUCCESS;
                }
            }
            return NodeStates.FAILURE;
        }
    }
}
