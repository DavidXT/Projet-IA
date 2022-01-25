using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : AState
{
    private AState _currentState;

    public StateMachine(GameObject ai, AState currentState) : base(ai)
    {
        _currentState = currentState;
    }

    public override void BeginState(StateMachine _sm)
    {
        _currentState.BeginState(this);
    }
    public override void UpdateState(StateMachine _sm)
    {
        _currentState.UpdateState(this);
        CheckState();
    }
    public override void EndState(StateMachine _sm)
    {
        _currentState.EndState(this);
    }

    private void CheckState()
    {
        AState tempState = _currentState.Check(this);
        if(tempState != null)
        {
            _currentState.EndState(this);
            _currentState = tempState;
            _currentState.BeginState(this);
        }
    }
}