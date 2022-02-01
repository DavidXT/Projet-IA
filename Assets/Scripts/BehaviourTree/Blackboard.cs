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
        public Vector3 targetLocation = Vector3.zero;
        public Vector3 zoneLocation = Vector3.zero;
        
        public float movementSpeed = 12f;

        public bool bIsOnPoint = false;
        public bool bIsReloading = false;

        private void OnEnable()
        {
            zoneLocation = GameObject.FindWithTag("Flag").gameObject.transform.position;
        }

        public object Clone()
        {
            Blackboard blackboard = CreateInstance<Blackboard>();
            blackboard.path = path;
            blackboard.targetLocation = targetLocation;
            blackboard.zoneLocation = zoneLocation;
            blackboard.movementSpeed = movementSpeed;
            blackboard.bIsOnPoint = bIsOnPoint;
            blackboard.bIsReloading = bIsReloading;
            return blackboard;
        }
    }
}
