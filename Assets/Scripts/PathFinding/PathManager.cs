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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        allTanks = GameObject.FindGameObjectsWithTag("Player");
    }

}
