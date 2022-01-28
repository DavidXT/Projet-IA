using System.Runtime.InteropServices.WindowsRuntime;

namespace Complete
{
    using UnityEngine;

    public class LookAtTarget : BTNode
    {
        private TankMovement _tankMovement = null;
    
        public LookAtTarget(TankMovement tankMovement)
        {
            _tankMovement = tankMovement;
        }
        public override NodeStates Evaluate()
        {
            Blackboard blackboard = _tankMovement.BehaviourTree.Blackboard;
            Transform tankTransorm = _tankMovement.transform;
            
            if (!Mathf.Approximately(Vector3.Angle(tankTransorm.forward, blackboard.targetLocation), 0))
            {
                Debug.Log("Rotate");
                tankTransorm.RotateAround(tankTransorm.position, tankTransorm.up, Vector3.Angle(tankTransorm.position, blackboard.targetLocation));
                return NodeStates.RUNNING;
            }

            return NodeStates.SUCCESS;
        }
    }
}