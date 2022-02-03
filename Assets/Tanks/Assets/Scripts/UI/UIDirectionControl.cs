using UnityEngine;

namespace Complete
{
    public class UIDirectionControl : MonoBehaviour
    {
        // This class is used to make sure world space UI
        // elements such as the health bar face the correct direction.

        public bool UseRelativeRotation = true;       // Use relative rotation should be used for this gameobject?


        private Quaternion RelativeRotation;          // The local rotatation at the start of the scene.


        private void Start()
        {
            RelativeRotation = transform.parent.localRotation;
        }


        private void Update()
        {
            if (UseRelativeRotation)
                transform.rotation = RelativeRotation;
        }
    }
}