using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public State currentState;
    public State Captured;
    public State Capturing;
    public State Idle;
    public float teamOnHellipad;
    public float teamOwner;
    public bool b_isCapturing;
    public bool b_isCaptured;
    public List<GameObject> nbPlayerOnHellipad;
    public float nbPlayer;

    public StateMachine(State _currentState)
    {
        currentState = _currentState;
    }

    void Start()
    {
        currentState = GetInitialState();
        currentState = Idle;
        teamOnHellipad = 0;
        teamOwner = 0;
        nbPlayer = 0;
        if (currentState != null)
            currentState.Enter();

    }

    void Update()
    {
        if (currentState != null)
            currentState.CheckState(this);
        nbPlayer = 0;
        foreach (GameObject go in PathManager.Instance.allTanks)
        {
            if (go.GetComponent<Complete.TankMovement>().b_onPoint)
            {
                nbPlayer++;
            }
        }
    }



    public void ChangeState(State newState)
    {
        foreach (Transition _transi in currentState.m_transition)
        {
            if(_transi.nextState == newState)
            {
                currentState.Exit();

                currentState = newState;
                currentState.Enter();
                break;
            }
        }
    }

    protected virtual State GetInitialState()
    {
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nbPlayerOnHellipad.Add(other.gameObject);
            if (currentState != Capturing && nbPlayerOnHellipad.Count == 1)
            {
                if (other.gameObject.GetComponent<Complete.TankMovement>().m_PlayerNumber != teamOwner)
                {
                    b_isCapturing = true;
                    ChangeState(Capturing);
                    teamOnHellipad = other.gameObject.GetComponent<Complete.TankMovement>().m_PlayerNumber;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nbPlayerOnHellipad.Remove(other.gameObject);
            if (currentState == Capturing && nbPlayerOnHellipad.Count == 0)
            {
                if (b_isCaptured)
                {
                    if(other.gameObject.GetComponent<Complete.TankMovement>().m_PlayerNumber != teamOwner)
                    {
                        b_isCapturing = false;
                        ChangeState(Capturing);
                        teamOnHellipad = 0;
                    }
                }else if (b_isCapturing)
                {
                    teamOnHellipad = 0;
                    b_isCapturing = false;
                }
            }

        }
    }

}
