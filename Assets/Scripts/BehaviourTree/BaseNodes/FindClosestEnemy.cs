using System.Collections.Generic;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "FindClosestEnemy", menuName = "BehaviourTree/Nodes/Tasks/FindClosestEnemy")]
    public class FindClosestEnemy : BTNode
    {
        private Blackboard Blackboard;

        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            float minDist =  10000;
            GameObject closestEnemy = null;
            GameObject[] allTanks = GameObject.FindGameObjectsWithTag("Player");
            if (allTanks.Length > 0)
            {
                foreach (GameObject tank in allTanks)
                {
                    float tempDist = Vector3.Distance(Blackboard.tankMovement.transform.position, tank.transform.position);
                    if (Blackboard.tankMovement.gameObject != tank && tempDist < minDist)
                    {
                        closestEnemy = tank;
                        minDist = tempDist;
                    }
                }
            }
            if (closestEnemy)
            {
                List<Vector3> path = Blackboard.tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position, closestEnemy.transform.position);
                
                if (path.Count > 0)
                {
                    Blackboard.targetTransform = closestEnemy.transform;
                    Blackboard.path = path;
                    return NodeStates.SUCCESS;
                }
            }
            return NodeStates.FAILURE;
        }

        public override object Clone()
        {
            FindClosestEnemy findClosestEnemy = CreateInstance<FindClosestEnemy>();
            return findClosestEnemy;
        }
    }
}