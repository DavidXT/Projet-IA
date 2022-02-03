using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Complete
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Blackboard", menuName = "BehaviourTree/Blackboard")]
    public class Blackboard : ScriptableObject, ICloneable
    {
        public TankMovement tankMovement = null;

        public Color playerColor;
        
        public List<Vector3> path = new List<Vector3>();
        
        public Transform tankTransform = null;
        public Transform targetTransform = null;
        public Transform zoneTransform = null;
        
        public float movementSpeed = 12f;

        public bool bIsOnPoint = false;
        public bool bIsReloading = false;

        public float acceptance = 0.999f; //valeur d'acceptance d'angle avant le shoot;

        public object Clone()
        {
            Blackboard blackboard = CreateInstance<Blackboard>();
            blackboard.path = path;
            blackboard.targetTransform = targetTransform;
            blackboard.zoneTransform = GameObject.FindGameObjectWithTag("Flag").transform;;
            blackboard.movementSpeed = movementSpeed;
            blackboard.bIsOnPoint = bIsOnPoint;
            blackboard.bIsReloading = bIsReloading;
            return blackboard;
        }
    }
}
