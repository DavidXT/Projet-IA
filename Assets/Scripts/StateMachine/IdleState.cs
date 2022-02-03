using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/StateMachine/State/IdleState", order = 2)]
public class IdleState : State
{
    public IdleState(StateMachine _sm, Transition[] _transition) : base(_sm, _transition)
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
        _sm.teamOwner = null;
        _sm.capturingTeam = null;
        if (_sm.nbPlayerOnHellipad.Count >= 1)
        {
            _sm.capturingTeam = _sm.nbPlayerOnHellipad[0].GetComponent<Complete.TankMovement>().m_Team;
            _sm.ChangeState(_sm.Capturing);
        }
    }
}
