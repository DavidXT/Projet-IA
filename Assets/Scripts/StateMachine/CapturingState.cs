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
    }
}
