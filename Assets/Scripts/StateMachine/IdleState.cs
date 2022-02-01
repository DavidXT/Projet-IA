using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/StateMachine/State/IdleState", order = 2)]
public class IdleState : State
{
    public IdleState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
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
        _sm.teamOwner = null;
        if (_sm.nbPlayerOnHellipad.Count >= 1)
        {
            _sm.ChangeState(_sm.Capturing);
        }
    }
}
