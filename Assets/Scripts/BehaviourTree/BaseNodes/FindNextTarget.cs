using System.Collections.Generic;

namespace Complete
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "FindNextTarget", menuName = "BehaviourTree/Nodes/Tasks/FindNextTarget")]
    public class FindNextTarget : BTNode
    {
        private Blackboard Blackboard;

        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
        
        public override NodeStates Evaluate()
        {
            float minDist = Vector3.Distance(Blackboard.tankTransform.position, Blackboard.zoneTransform.position);
            
            GameObject closestEnemy = null;
            GameObject[] allTanks = GameObject.FindGameObjectsWithTag("Player");
            
            if (allTanks.Length > 0)
            {
                foreach (GameObject tank in allTanks)
                {
                    float tempDist = Vector3.Distance(Blackboard.tankTransform.position, tank.transform.position);
                    if (Blackboard.tankMovement.gameObject != tank && tempDist < minDist && Blackboard.tankMovement.m_Team != tank.GetComponent<TankMovement>().m_Team)
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
                else
                {
                    return NodeStates.FAILURE;
                }
            }
            else
            {
                List<Vector3> path = Blackboard.tankMovement.MovementMode.GetPathToLocation(Blackboard.tankTransform.position, Blackboard.zoneTransform.position);
                if (path.Count > 0)
                {
                    Blackboard.path = path;
                    Blackboard.targetTransform = Blackboard.zoneTransform;
                    return NodeStates.SUCCESS;
                }
                else
                {
                    return NodeStates.FAILURE;
                }
            }
        }

        public override object Clone()
        {
            FindNextTarget findNextTarget = CreateInstance<FindNextTarget>();
            return findNextTarget;
        }
    }
}