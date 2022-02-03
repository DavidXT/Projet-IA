using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete
{
    [CreateAssetMenu(fileName = "FindZone", menuName = "BehaviourTree/Nodes/Tasks/FindZone")]
    public class FindZone : BTTask
    {
        public override object Clone()
        {
            return CreateInstance<FindZone>();
        }

        public override NodeStates Evaluate()
        {
            if (Blackboard)
                if (Blackboard.zoneTransform)
                {
                    Debug.Log("OUIOUIOUI");
                    Blackboard.targetTransform = Blackboard.zoneTransform;
                    Blackboard.path =
                        Blackboard.tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position,
                            Blackboard.targetTransform.position);
                    return NodeStates.SUCCESS;
                }
            return NodeStates.FAILURE;
        }
    }

}
