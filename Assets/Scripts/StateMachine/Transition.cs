using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Transition", menuName = "ScriptableObjects/StateMachine/Transition", order = 2)]
public class Transition : ScriptableObject
{
    public State nextState;
    
    public Transition(State _nextState)
    {
        nextState = _nextState;
    }

    public bool Check(State _state)
    {
        if(_state == nextState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
