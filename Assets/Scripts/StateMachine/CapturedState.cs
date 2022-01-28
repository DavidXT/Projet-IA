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

    public override void CheckState(StateMachine _sm)
    {
        base.CheckState(_sm);
        //TO DO
    }

}
