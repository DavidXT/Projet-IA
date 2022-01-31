using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContestedState", menuName = "ScriptableObjects/StateMachine/State/ContestedState", order = 4)]
public class ContestedState : State
{
    public ContestedState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
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
        //TO DO
    }
}
