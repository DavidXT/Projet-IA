using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ScriptableObjects/StateMachine/State", order = 1)]
public class State : ScriptableObject
{
    // Start is called before the first frame update

    protected StateMachine m_stateMachine;
    public Transition[] m_transition;

    public State(StateMachine _sm, Transition[] _transition)
    {
        this.m_transition = _transition;
        this.m_stateMachine = _sm;
    }

    public virtual void Enter() { }
    public virtual void CheckState(StateMachine _sm) { }
    public virtual void Exit() { }
}
