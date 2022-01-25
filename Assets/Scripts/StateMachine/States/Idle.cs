using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Idle")]
public class Idle : AState
{
    private CaptureZone Zone;
    public Idle(GameObject ai) : base(ai)
    {
    }

    public override void BeginState(StateMachine sm)
    {
        Zone = sm.AI.GetComponent<CaptureZone>();
    }

    public override void UpdateState(StateMachine sm)
    {
        if(Zone.Points > 0)
            Zone.Points -= Time.deltaTime;
    }

    public override void EndState(StateMachine sm)
    {
        
    }
}
