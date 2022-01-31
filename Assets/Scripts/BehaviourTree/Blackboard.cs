using System;
using UnityEngine.Serialization;

namespace Complete
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Blackboard", menuName = "BehaviourTree/Blackboard")]
    public class Blackboard : ScriptableObject
    {
        public TankMovement tankMovement = null;
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
    }
}
