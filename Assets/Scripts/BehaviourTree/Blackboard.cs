using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blackboard", menuName = "BehaviourTree/Blackboard")]
public class Blackboard : ScriptableObject
{
    public Vector3 position;
    public int nodesPassed = 0;
}
