using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Transitions/PointsIdle")]
public class TransitionPointsIdle : ATransition
{
    public TransitionPointsIdle(AState nextState) : base(nextState)
    {
    }

    public override bool Check(StateMachine sm)
    {
        return !sm.AI.GetComponent<CaptureZone>().TankOnZone;
    }
}
