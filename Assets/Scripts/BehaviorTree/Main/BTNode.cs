using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Node", menuName = "ScriptableObjects/BehaviorTree/Node", order = 1)]
public abstract class BTNode : ScriptableObject
{
    public enum NodeState { RUNNING, SUCCESS, FAILURE }
    public NodeState state = NodeState.RUNNING;
    public bool started = false;
    public BTNode parent;
    protected List<BTNode> children = new List<BTNode>();

    public BTNode()
    {
        parent = null;
    }

    public NodeState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if(state == NodeState.FAILURE || state == NodeState.SUCCESS)
        {
            OnStop();
            started = false;
        }
        return state;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeState OnUpdate();

}
