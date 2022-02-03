namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "IsEnemyNearby", menuName = "BehaviourTree/Nodes/Tasks/IsEnemyNearby")]
    public class IsEnemyNearby : BTNode
    {
        private Blackboard Blackboard;

        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            if (Vector3.Distance(Blackboard.tankTransform.position, Blackboard.targetTransform.position) - Vector3.Distance(Blackboard.tankTransform.position, Blackboard.zoneLocation) > 2f)
            {
                Debug.Log("Enemy nearby");
                return NodeStates.SUCCESS;
            }
                Debug.Log("No enemy nearby");
            return NodeStates.FAILURE;
        }

        public override object Clone()
        {
            IsEnemyNearby isEnemyNearby = CreateInstance<IsEnemyNearby>();
            return isEnemyNearby;
        }
    }
}