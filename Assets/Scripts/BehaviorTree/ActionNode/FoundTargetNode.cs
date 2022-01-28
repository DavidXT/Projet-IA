using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundTargetNode : ActionNode
{
    public GameObject Tank;
    FoundTargetNode(GameObject _tank)
    {
        Tank = _tank;
    }
    protected override void OnStart()
    {
        Tank.GetComponent<Complete.TankMovement>().SearchTarget();
        Pathfinding.Instance.AStar(Tank.transform.position, Tank.GetComponent<Complete.TankShooting>().m_target.transform.position);
        Tank.GetComponent<Complete.TankMovement>().pathNode = Grid.Instance.path;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {

        return NodeState.SUCCESS;
    }
}
