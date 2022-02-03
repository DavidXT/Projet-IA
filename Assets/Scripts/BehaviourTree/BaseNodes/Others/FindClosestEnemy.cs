using System.Collections.Generic;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "FindClosestEnemy", menuName = "BehaviourTree/Nodes/Tasks/FindClosestEnemy")]
    public class FindClosestEnemy : BTTask
    {
        public override NodeStates Evaluate()
        {
            float minDist = 100000;
            GameObject closestEnemy = null;
            GameObject[] allTanks = GameObject.FindGameObjectsWithTag("Player");
            if (allTanks.Length > 0)
            {
                for (int i = 0; i < allTanks.Length; i++)
                {
                    float tempDist = Vector3.Distance(Blackboard.tankMovement.transform.position, allTanks[i].transform.position);
                    if (Blackboard.tankMovement.gameObject != allTanks[i] && tempDist < minDist && Blackboard.tankMovement.m_Team != allTanks[i].GetComponent<TankMovement>().m_Team)
                    {
                        closestEnemy = allTanks[i];
                        minDist = tempDist;
                    }
                }
            }
            if (closestEnemy)
            {
                List<Vector3> path = Blackboard.tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position, closestEnemy.transform.position);
                if (path.Count > 0)
                {
                    Blackboard.closestEnemy = closestEnemy.transform;
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