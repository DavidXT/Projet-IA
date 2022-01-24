using UnityEngine;

public abstract class TankMovementMode : ScriptableObject
{
    public abstract Vector3 GetNextLocation(Vector3 from, Vector3 target, int agentID = 0);
}
