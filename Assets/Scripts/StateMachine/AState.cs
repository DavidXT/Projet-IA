using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState : ScriptableObject
{
    public GameObject AI;
    public ATransition[] Transitions;

    public AState(GameObject ai)
    {
        AI = ai;
        Transitions = new ATransition[0];
    }

    public virtual AState Check(StateMachine sm)
    {
        if (Transitions.Length == 0) return null;

        foreach (ATransition transition in Transitions)
        {
            if (transition.Check(sm))
            {
                return transition.NextState;
            }
        }
        return null;
    }
    
    public abstract void BeginState(StateMachine sm);
    public abstract void UpdateState(StateMachine sm);
    public abstract void EndState(StateMachine sm);
}
