using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CapturingState", menuName = "ScriptableObjects/StateMachine/State/CapturingState", order = 2)]
public class CapturingState : State
{

    public override void Enter()
    {
        base.Enter();
    }
    public CapturingState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
    {
        this.m_stateMachine = _sm;
        this.m_transition = _transition;
    }

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        if (_sm.currentState == _sm.Capturing)
        {
            if (_sm.canCapture == true)
            {
                if (_sm.nbPlayerOnHellipad.Count > 0)
                {
                    if (_sm.currCaptureBar < _sm.captureValue)
                    {
                        _sm.currCaptureBar += Time.deltaTime;
                        if (_sm.currCaptureBar > _sm.captureValue)
                        {
                            _sm.ChangeState(_sm.Captured);
                            _sm.teamOwner = _sm.currTeam;
                        }
                    }
                }
                else
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

            }
            else
            {
                _sm.ChangeState(_sm.Contested);
            }
        }
    }
}
