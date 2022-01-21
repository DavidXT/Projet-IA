using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIA : MonoBehaviour
{
    private StateMachine m_StateMachine;
    private bool m_IsActive = false;
    
    public void InitSM()
    {
        m_StateMachine = CreateSM();
        m_StateMachine.BeginState();
        m_IsActive = true;
    }

    private StateMachine CreateSM()
    {
        //SM1
        Init tempInit = new Init(gameObject);
        Move tempMove = new Move(gameObject);

        StateMachine tempSM1 = new StateMachine(gameObject, tempInit);

        TransitionInitMove tempInitWalk = new TransitionInitMove(tempMove);


        tempInit.Transitions = new ATransition[1] { tempInitWalk };

        //SM0
        //Death tempDeath = new Death(gameObject);

        //StateMachine tempSM0 = new StateMachine(gameObject, tempSM1);

        //TransitionSM1Death tempSM1Death = new TransitionSM1Death(tempDeath, this);

        //tempSM1.Transitions = new ATransition[1] { tempSM1Death };

        return tempSM1;
    }

    private void Update()
    {
        if (m_IsActive)
        {
            m_StateMachine.UpdateState();
        }
    }
}