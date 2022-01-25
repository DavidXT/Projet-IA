using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/DeliverPoints")]
public class DeliverPoints : AState
{
    public DeliverPoints(GameObject ai) : base(ai)
    {
    }

    public override void BeginState(StateMachine sm)
    {
        Debug.Log("Deliver start");
    }

    public override void UpdateState(StateMachine sm)
    {
        Debug.Log("Delivering...");
    }

    public override void EndState(StateMachine sm)
    {
        
    }
}
