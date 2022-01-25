using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/DeliverPoints")]
public class DeliverPoints : AState
{
    private CaptureZone Zone;
    public DeliverPoints(GameObject ai) : base(ai)
    {
    }

    public override void BeginState(StateMachine sm)
    {
        Zone = sm.AI.GetComponent<CaptureZone>();
    }

    public override void UpdateState(StateMachine sm)
    {
        Zone.Points += Zone.TanksOnZone * Time.deltaTime;
    }

    public override void EndState(StateMachine sm)
    {
        
    }
}
