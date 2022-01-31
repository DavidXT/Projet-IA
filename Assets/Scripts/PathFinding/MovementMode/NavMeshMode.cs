using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Movement/NavMeshMode")]
public class NavMeshMode : TankMovementMode
{
    public override List<Vector3> GetPathToLocation(Vector3 from, Vector3 target, int agentID = 0)
    {
        NavMeshQueryFilter navMeshQueryFilter = new NavMeshQueryFilter()
        {
            areaMask = NavMesh.AllAreas,
            agentTypeID = agentID
        };
        NavMeshPath path = new NavMeshPath();

        if (NavMesh.CalculatePath(from, target, navMeshQueryFilter, path))
        {
            return path.corners.ToList();
        }

        return new List<Vector3>() {from, target};
    }
}