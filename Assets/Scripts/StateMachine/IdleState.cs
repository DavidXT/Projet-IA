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

}
