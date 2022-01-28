using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ScriptableObjects/StateMachine/State", order = 1)]
public class State : ScriptableObject
{
    // Start is called before the first frame update

    protected StateMachine stateMachine;
    public Transition[] transition;

    public State(StateMachine _sm, Transition[] _transition)
    {
        this.transition = _transition;
        this.stateMachine = _sm;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Enter() { }
    public virtual void CheckState(StateMachine _sm) { }
    public virtual void Exit() { }
}
