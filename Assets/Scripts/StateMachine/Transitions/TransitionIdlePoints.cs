using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Transitions/IdlePoints")]
public class TransitionIdlePoints : ATransition
{
    public TransitionIdlePoints(AState nextState) : base(nextState)
    {
    }

    public override bool Check(StateMachine sm)
    {
        return sm.AI.GetComponent<CaptureZone>().TankOnZone;
    }
}
