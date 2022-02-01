using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{

    public State currentState;
    public State Captured;
    public CapturingState Capturing;
    public State Contested;

    public State Neutral;
    public float currCaptureBar;
    public float captureValue;
    public Image fillBar;

    public int teamOnHellipad;
    public int nbTeamOnHellipad;
    public int teamOwner;
    public int currTeam;
    public bool canCapture = false;
    public List<GameObject> nbPlayerOnHellipad;


    public StateMachine(State _currentState)
    {
        currentState = _currentState;
    }

    void Start()
    {
        currentState = GetInitialState();
        currentState = Neutral;
        currCaptureBar = 0;
        captureValue = 10;
        teamOnHellipad = 0; 
        currTeam = 0;
         teamOwner = 0;;
        if (currentState != null)
            currentState.Enter();

    }

    void Update()
    {
        checkTanks();
        canCapture = checkTeamOnHellipad();
        fillBar.fillAmount = currCaptureBar / captureValue;
        if (currentState != null)
            currentState.CheckState(this);
    }

    public void checkTanks()
    {
        if(nbPlayerOnHellipad.Count > 0)
        {
            foreach (GameObject go in nbPlayerOnHellipad.ToArray())
            {
                if (go.activeSelf == false)
                {
                    nbPlayerOnHellipad.Remove(go);
                }
            }
        }
    }

    public bool checkTeamOnHellipad()
    {
        if(nbPlayerOnHellipad.Count > 1)
        {
            for (int i = 1; i < nbPlayerOnHellipad.Count; i++)
            {
                if(nbPlayerOnHellipad[0].GetComponent<Complete.TankMovement>().m_PlayerNumber != nbPlayerOnHellipad[i].GetComponent<Complete.TankMovement>().m_PlayerNumber)
                {
                    return false;
                }
                currTeam = nbPlayerOnHellipad[0].GetComponent<Complete.TankMovement>().m_PlayerNumber;
            }
        }
        if(nbPlayerOnHellipad.Count == 1)
        {
            currTeam = nbPlayerOnHellipad[0].GetComponent<Complete.TankMovement>().m_PlayerNumber;
        }
        return true;
    }

    public bool checkOwner()
    {
        if (nbPlayerOnHellipad.Count >= 1)
        {
            for (int i = 0; i < nbPlayerOnHellipad.Count; i++)
            {
                if (teamOwner == nbPlayerOnHellipad[i].GetComponent<Complete.TankMovement>().m_PlayerNumber)
                {
                    return true;
                }
            }
        }
        return false;
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
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nbPlayerOnHellipad.Remove(other.gameObject);
        }
    }
}
