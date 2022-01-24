using UnityEngine;

[CreateAssetMenu(menuName = "Movement/GridModeDijkstra")]
public class GridModeDijkstra : TankMovementMode
{
    public override Vector3 GetNextLocation(Vector3 from, Vector3 target, int agentID = 0)
    {
        
        Pathfinding.Instance.Dijkstra(from, target);
        return Grid.Instance.path[0].worldPosition;
    }
}
