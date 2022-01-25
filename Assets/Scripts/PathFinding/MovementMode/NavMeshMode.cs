using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Movement/NavMeshMode")]
public class NavMeshMode : TankMovementMode
{
    public override Vector3 GetNextLocation(Vector3 from, Vector3 target, int agentID = 0)
    {
        NavMeshQueryFilter navMeshQueryFilter = new NavMeshQueryFilter()
        {
            areaMask = NavMesh.AllAreas,
            agentTypeID = agentID
        };
        NavMeshPath path = new NavMeshPath();

        if (NavMesh.CalculatePath(from, target, navMeshQueryFilter, path))
        {
            return path.corners[1];
        }
        return from;
    }
}
