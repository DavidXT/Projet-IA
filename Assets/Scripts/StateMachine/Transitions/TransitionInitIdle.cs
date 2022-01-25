using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Transitions/InitIdle")]
public class TransitionInitIdle : ATransition
{
    public TransitionInitIdle(AState nextState) : base(nextState)
    {
    }

    public override bool Check(StateMachine sm)
    {
        return true;
    }
}
