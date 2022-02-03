using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public Pathfinding pathfinding;
    public Grid grid;
    public GameObject[] allTanks;
    public GameObject targetPoint;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
    }

    void Start()
    {
        allTanks = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        allTanks = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in allTanks)
        {
            GetNextLocation(allTanks, go);
            //pathfinding.AStar(go.transform.position, go.GetComponent<Complete.TankShooting>().target.transform.position);
            //go.GetComponent<Complete.TankMovement>().path = Grid.Instance.path;
        }
    }

    //Return the next location the tank will want to go
    //Will be replaced by the behaviour tree
    void GetNextLocation(GameObject[] _tankList, GameObject _currentTank)
    {
        float currDistance = 1000;
        if (!_currentTank.gameObject.GetComponent<Complete.TankMovement>().b_onPoint)
        {
            currDistance = Vector3.Distance(_currentTank.transform.position, targetPoint.transform.position);
            _currentTank.GetComponent<Complete.TankShooting>().target = targetPoint.transform;
        }
        for (int i = 0; i < _tankList.Length; i++)
        {
            if (Vector3.Distance(_tankList[i].transform.position,_currentTank.transform.position) < currDistance)
            {
                if(_currentTank != _tankList[i])
                {
                    currDistance = Vector3.Distance(_tankList[i].transform.position, _currentTank.transform.position);
                    _currentTank.GetComponent<Complete.TankShooting>().target = _tankList[i].transform;
                    _currentTank.GetComponent<Complete.TankMovement>().BehaviourTree.Blackboard.targetTransform = _tankList[i].transform;
                }
            }
        }
    }
}
