using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Pathfinding pathfinding;
    public Grid grid;
    public int speed = 10;
    void Awake()
    {
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
        //if(grid.path != null)
        //{
        //    if(grid.path.Count > 0)
        //    {
        //        pathfinding.seeker.transform.position = Vector3.MoveTowards(pathfinding.seeker.transform.position, grid.path[0].worldPosition, speed * Time.deltaTime);
        //    }
        //}
        if (pathfinding.target != null)
        {
            pathfinding.seeker.transform.LookAt(pathfinding.target.transform.position);
        }
    }
}
