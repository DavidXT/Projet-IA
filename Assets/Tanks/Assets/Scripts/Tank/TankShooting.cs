using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        [SerializeField] public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public Transform m_target;
public float m_shootDistance;
        private Rigidbody m_Rigidbody;
        RaycastHit hit;

        private string m_FireButton;                // The input axis that is used for launching shells.
        [SerializeField] private float m_CurrentLaunchForce = 50;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
        [SerializeField] private SO_Cooldown m_soCooldown;
        [SerializeField] private float m_resetCooldown;
        [SerializeField] public float m_currCooldown;


        private void OnEnable()
        {
        }


        private void Start ()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            if (m_PlayerNumber <= 2)
            {
                // The fire axis is based on the player number.
                m_FireButton = "Fire" + m_PlayerNumber;
            }

            m_shootDistance = 50;
            m_currCooldown = 0;

        //m_soCooldown.fCooldown = 0;
        }


        private void Update ()
        {

            if (m_PlayerNumber <= 2)
            {
                if (Input.GetButtonDown(m_FireButton) && m_currCooldown < 0)
                {
                    Fire();
                }
            }
            if (m_currCooldown > 0)
            {
                m_currCooldown -= Time.deltaTime;
            }

        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, m_shootDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (m_currCooldown <= 0)
                    {
                        Fire();
                        //Rotate tank around target
                        Quaternion turnRotation = Quaternion.Euler(0f, 90f, 0f);
                        // Apply this rotation to the rigidbody's rotation.
                        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
                    }
                }
            }
        }



        //void OnDrawGizmos()
        //{
        //    // Draws a 5 unit long red line in front of the object
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        //}


        private void Fire ()
        {
            // Set the fired flag so only Fire is only called once.
            m_Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();

            //m_soCooldown.fCooldown = m_resetCooldown;
            m_currCooldown = m_resetCooldown;

        }
    }
}