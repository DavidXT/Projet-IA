using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CapturedState", menuName = "ScriptableObjects/StateMachine/State/CapturedState", order = 2)]
public class CapturedState : State
{
    public CapturedState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
    {
        this.m_stateMachine = _sm;
        this.m_transition = _transition;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        _sm.teamOwner.m_TeamScore += Time.deltaTime;
        if (!_sm.checkOwner())
        {
            _sm.currCaptureBar -= Time.deltaTime;
            if (_sm.currCaptureBar <= 0)
            {
                _sm.ChangeState(_sm.Neutral);
            }
        }
        else
        {
            if (_sm.currCaptureBar < _sm.captureValue)
            {
                _sm.currCaptureBar += Time.deltaTime;
            }
        }
    }

}
