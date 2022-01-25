using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    [SerializeField] private AState m_StartingState = null;
    private StateMachine m_StateMachine = null;
    private bool m_IsActive = false;

    public float Points = 0;
    
    private List<GameObject> _tanksOnZone;
    public bool TankOnZone => _tanksOnZone.Count > 0;
    public int TanksOnZone => _tanksOnZone.Count;

    void Start()
    {
        _tanksOnZone = new List<GameObject>();
        InitSM();
    }
    
    private void InitSM()
    {
        m_StateMachine = CreateSM();
        m_IsActive = true;
        m_StateMachine.BeginState(m_StateMachine);
    }

    private StateMachine CreateSM()
    {
        //SM1
        StateMachine tempSM1 = new StateMachine(gameObject, m_StartingState);

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
            m_StateMachine.UpdateState(m_StateMachine);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tank enter");
            if (!_tanksOnZone.Contains(other.gameObject))
            {
                _tanksOnZone.Add(other.gameObject);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tank exit");
            if (_tanksOnZone.Contains(other.gameObject))
            {
                _tanksOnZone.Remove(other.gameObject);
            }
        }
    }

}