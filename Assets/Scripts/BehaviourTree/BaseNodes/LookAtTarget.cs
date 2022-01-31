namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "LookAtTarget", menuName = "BehaviourTree/Nodes/Tasks/LookAtTarget")]
    public class LookAtTarget : BTNode
    {
        [SerializeField] private Blackboard blackboard;

        public override NodeStates Evaluate()
        {
            TankMovement tankMovement = blackboard.tankMovement;
            Transform tankTransform = tankMovement.transform;
            
            if (!Mathf.Approximately(Vector3.Dot(tankTransform.forward, (blackboard.targetLocation - tankTransform.position).normalized), 1))
            {
                tankTransform.RotateAround(tankTransform.position, tankTransform.up, Vector3.Angle(tankTransform.forward, (blackboard.targetLocation - tankTransform.position).normalized) * Time.deltaTime);
                return NodeStates.RUNNING;
            }

            return NodeStates.SUCCESS;
        }
    }
}