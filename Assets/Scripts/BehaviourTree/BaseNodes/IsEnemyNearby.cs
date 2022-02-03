namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "IsEnemyNearby", menuName = "BehaviourTree/Nodes/Tasks/IsEnemyNearby")]
    public class IsEnemyNearby : BTTask
    {
        
        public override NodeStates Evaluate()
        {
            if (Vector3.Distance(Blackboard.tankTransform.position, Blackboard.targetTransform.position) - Vector3.Distance(Blackboard.tankTransform.position, Blackboard.zoneLocation) > 2f)
            {
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }

        public override object Clone()
        {
            IsEnemyNearby isEnemyNearby = CreateInstance<IsEnemyNearby>();
            return isEnemyNearby;
        }
    }
}