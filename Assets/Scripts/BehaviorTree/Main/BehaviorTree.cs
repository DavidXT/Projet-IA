using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject
{
    public BTNode root;
    public BTNode.NodeState treeState = BTNode.NodeState.RUNNING;
    
    public BTNode.NodeState Update()
    {
        if(root.state == BTNode.NodeState.RUNNING)
        {
            treeState = root.Update();
        }
        return treeState;

    }
}
