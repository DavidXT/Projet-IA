using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CapturingState", menuName = "ScriptableObjects/StateMachine/State/CapturingState", order = 2)]
public class CapturingState : State
{
    public float m_captureTime;
    public float m_currentTime;

    public override void Enter()
    {
        base.Enter();
        m_currentTime = 0;
    }
    public CapturingState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
    {
        this.m_stateMachine = _sm;
        this.m_transition = _transition;
    }

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        if (_sm.b_isCapturing && _sm.teamOnHellipad != 0)
        {
            m_currentTime += Time.deltaTime;
            if (m_captureTime <= m_currentTime)
            {
                _sm.ChangeState(m_transition[0].nextState);
                _sm.teamOwner = _sm.teamOnHellipad;
            }
        }
        else
        {
            if(m_currentTime > 0)
            {
                m_currentTime -= Time.deltaTime;
            }
            if(m_currentTime < 0)
            {
                if(_sm.teamOwner != 0)
                {
                    _sm.ChangeState(m_transition[0].nextState);
                }
                else
                {
                    _sm.ChangeState(m_transition[1].nextState);
                }
            }
        }


    }
}
