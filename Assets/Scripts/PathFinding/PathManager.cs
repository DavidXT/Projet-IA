using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public Pathfinding pathfinding;
    public Grid grid;
    public GameObject[] allTanks;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        allTanks = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in allTanks)
        {
            GetDistanceTank(allTanks, go);
            pathfinding.AStar(go.transform.position, go.GetComponent<Complete.TankShooting>().m_target.transform.position, go.GetComponent<Complete.TankMovement>().path);
            go.GetComponent<Complete.TankMovement>().path = Grid.Instance.path;
        }
    }

    void GetDistanceTank(GameObject[] _tankList, GameObject _currentTank)
    {
        float currDistance = 1000;
        for(int i = 0; i < _tankList.Length; i++)
        {
            if (Vector3.Distance(_tankList[i].transform.position,_currentTank.transform.position) < currDistance && currDistance > 0)
            {
                if(_currentTank != _tankList[i])
                {
                    currDistance = Vector3.Distance(_tankList[i].transform.position, _currentTank.transform.position);
                    _currentTank.GetComponent<Complete.TankShooting>().m_target = _tankList[i].transform;
                }
            }
        }
    }
}
