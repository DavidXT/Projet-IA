using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Init")]
public class Init : AState
{
    public Init(GameObject ai) : base(ai)
    {
    }

    public override void BeginState(StateMachine sm)
    {
        Debug.Log("Init");
    }

    public override void UpdateState(StateMachine sm)
    {
        
    }

    public override void EndState(StateMachine sm)
    {
        
    }
}
