using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody Shell;                   // Prefab of the shell.
        [SerializeField] public Transform FireTransform;           // A child of the tank where the shells are spawned.
        public Slider AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip FireClip;                // Audio that plays when each shot is fired.
        public Transform target;
public float shootDistance;
        private Rigidbody Rigidbody;
        RaycastHit hit;

        private string FireButton;                // The input axis that is used for launching shells.
        [SerializeField] private float CurrentLaunchForce = 50;         // The force that will be given to the shell when the fire button is released.
        private float ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool Fired;                       // Whether or not the shell has been launched with this button press.
        [SerializeField] private SO_Cooldown soCooldown;
        [SerializeField] private float resetCooldown;
        [SerializeField] public float currCooldown;


        private void OnEnable()
        {
        }


        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            if (PlayerNumber <= 2)
            {
                // The fire axis is based on the player number.
                FireButton = "Fire" + PlayerNumber;
            }

            shootDistance = 50;
            currCooldown = 0;

        //soCooldown.fCooldown = 0;
        }


        private void Update()
        {
            if (PlayerNumber <= 2)
            {
                if (Input.GetButtonDown(FireButton) && currCooldown < 0)
                {
                    Fire();
                }
            }
            if (currCooldown > 0)
            {
                currCooldown -= Time.deltaTime;
            }

        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, shootDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (currCooldown <= 0)
                    {
                        Fire();
                        //Rotate tank around target
                        Quaternion turnRotation = Quaternion.Euler(0f, 90f, 0f);
                        // Apply this rotation to the rigidbody's rotation.
                        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
                    }
                }
            }
        }

        //void OnDrawGizmos()
        //{
        //    // Draws a 5 unit long red line in front of the object
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        //}


        private void Fire()
        {
            // Set the fired flag so only Fire is only called once.
            Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate (Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = CurrentLaunchForce * FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            ShootingAudio.clip = FireClip;
            ShootingAudio.Play();

            //soCooldown.fCooldown = resetCooldown;
            currCooldown = resetCooldown;

        }
    }
}