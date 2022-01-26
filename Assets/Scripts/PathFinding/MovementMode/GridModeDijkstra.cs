using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/GridModeDijkstra")]
public class GridModeDijkstra : TankMovementMode
{
    public override List<Vector3> GetPathToLocation(Vector3 from, Vector3 target, int agentID = 0)
    {
        Pathfinding.Instance.Dijkstra(from, target);
        
        List<Vector3> path = new List<Vector3>();
        Grid.Instance.path.ForEach((e) => { path.Add(e.worldPosition);});
        
        return path;
    }
}
