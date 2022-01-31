using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : ActionNode
{
    public GameObject Tank;
    protected override void OnStart()
    {
        //TODO
    }

    protected override void OnStop()
    {
        //TODO
    }

    protected override NodeState OnUpdate()
    {
        Tank.GetComponent<Complete.TankMovement>().IAMoveTo();
        return NodeState.SUCCESS;
    }
}
