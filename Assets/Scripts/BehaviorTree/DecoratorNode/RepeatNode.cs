using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
       
    }

    protected override NodeState OnUpdate()
    {
        child.Update();
        return NodeState.RUNNING;
    }
}

