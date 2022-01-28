using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CapturingState", menuName = "ScriptableObjects/StateMachine/State/CapturingState", order = 2)]
public class CapturingState : State
{
    public float captureTime;
    public float currentTime;

    public override void Enter()
    {
        base.Enter();
        currentTime = 0;
    }
    public CapturingState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
    {
        this.stateMachine = _sm;
        this.transition = _transition;
    }

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        if (_sm.b_isCapturing && _sm.teamOnHellipad != 0)
        {
            currentTime += Time.deltaTime;
            if (captureTime <= currentTime)
            {
                _sm.ChangeState(transition[0].nextState);
                _sm.teamOwner = _sm.teamOnHellipad;
            }
        }
        else
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            if(currentTime < 0)
            {
                if(_sm.teamOwner != 0)
                {
                    _sm.ChangeState(transition[0].nextState);
                }
                else
                {
                    _sm.ChangeState(transition[1].nextState);
                }
            }
        }


    }
}
