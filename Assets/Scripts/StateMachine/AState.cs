using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    public ATransition[] Transitions;

    protected GameObject _ai;

    public AState(GameObject ai)
    {
        _ai = ai;
        Transitions = new ATransition[0];
    }

    public virtual AState Check()
    {
        foreach(ATransition transition in Transitions)
        {
            if(transition.Check())
            {
                return transition.NextState;
            }
        }
        return null;
    }

    public abstract void BeginState();
    public abstract void UpdateState();
    public abstract void EndState();
}