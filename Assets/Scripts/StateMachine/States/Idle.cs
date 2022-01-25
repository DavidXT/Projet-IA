using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Idle")]
public class Idle : AState
{
    public Idle(GameObject ai) : base(ai)
    {
    }

    public override void BeginState(StateMachine sm)
    {
        Debug.Log("Idle");
    }

    public override void UpdateState(StateMachine sm)
    {
        
    }

    public override void EndState(StateMachine sm)
    {
        
    }
}
