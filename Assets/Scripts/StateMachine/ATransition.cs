using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATransition
{
    public AState NextState;

    public ATransition(AState nextState)
    {
        NextState = nextState;
    }

    public abstract bool Check();
}