using System.Collections.Generic;
using UnityEngine;

public abstract class TankMovementMode : ScriptableObject
{
    public abstract List<Vector3> GetPathToLocation(Vector3 from, Vector3 target, int agentID = 0);
}
