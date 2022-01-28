using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blackboard", menuName = "BehaviourTree/Blackboard")]
public class Blackboard : ScriptableObject
{
    public Vector3 position = Vector3.zero;
    public Vector3 targetLocation = Vector3.zero;
    
    public float movementSpeed = 12f;

    public bool bIsOnPoint = false;
    public bool bIsReloading = false;
    public int nodesPassed = 0;
}
