using UnityEngine;

[CreateAssetMenu(menuName = "Movement/GridModeAStar")]
public class GridModeAStar : TankMovementMode
{
    public override Vector3 GetNextLocation(Vector3 from, Vector3 target, int agentID = 0)
    {
        Pathfinding.Instance.AStar(from, target);
       return Grid.Instance.path[0].worldPosition;
    }
}
