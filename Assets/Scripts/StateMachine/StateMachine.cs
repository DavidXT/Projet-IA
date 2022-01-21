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

    public override void BeginState()
    {
        _currentState.BeginState();
    }
    public override void UpdateState()
    {
        _currentState.UpdateState();
        CheckState();
    }
    public override void EndState()
    {
        _currentState.EndState();
    }


    private void CheckState()
    {
        AState tempState = _currentState.Check();
        if(tempState != null)
        {
            _currentState.EndState();
            _currentState = tempState;
            _currentState.BeginState();
        }
    }
}