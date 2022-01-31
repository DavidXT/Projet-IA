using System;

namespace Complete
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Blackboard", menuName = "BehaviourTree/Blackboard")]
    public class Blackboard : ScriptableObject, ICloneable
    {
        public TankMovement tankMovement = null;
        
        public Vector3[] path = new Vector3[0];
        
        public Vector3 position = Vector3.zero;
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
            blackboard.tankMovement = tankMovement;
            blackboard.path = path;
            blackboard.position = position;
            blackboard.targetLocation = targetLocation;
            blackboard.zoneLocation = zoneLocation;
            blackboard.movementSpeed = movementSpeed;
            blackboard.bIsOnPoint = bIsOnPoint;
            blackboard.bIsReloading = bIsReloading;
            return blackboard;
        }
    }
}
