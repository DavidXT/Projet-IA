using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;
    protected override void OnStart()
    {
        Debug.Log("On est la");
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        return NodeState.SUCCESS;
    }
}
