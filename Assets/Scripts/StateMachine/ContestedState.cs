using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContestedState", menuName = "ScriptableObjects/StateMachine/State/ContestedState", order = 4)]
public class ContestedState : State
{
    public ContestedState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
    {
        this.stateMachine = _sm;
        this.transition = _transition;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        if (_sm.canCapture == false)
        {
            if (_sm.currCaptureBar >= 0)
            {
                _sm.currCaptureBar -= Time.deltaTime;
                if (_sm.currCaptureBar <= 0)
                {
                    _sm.ChangeState(_sm.Neutral);
                }
            }
        }
        else
        {
            _sm.ChangeState(_sm.Capturing);
        }
    }
}
