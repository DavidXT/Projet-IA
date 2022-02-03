using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public SO_Team m_Team;
        public float respawnTime = 5;

        public float m_Speed = 12f;                 // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
        public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
		public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
        public bool m_IsIA = false;
        public float m_moveDistance = 5;


        private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;              // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.
        private float m_MovementInputValue;         // The current value of the movement input.
        private float m_TurnInputValue;             // The current value of the turn input.
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks
        public List<Vector3> path;
        public List<Node> pathNode;
        public float distanceShoot;
        public float currDistance = 1000;

        [SerializeField] private TankMovementMode m_MovementMode;

        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
        }


        private void OnEnable ()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }
        }


        private void OnDisable ()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for(int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }


        private void Start ()
        {
            distanceShoot = 20;
            currDistance = 1000;
            // The axes names are based on player number.
            if(m_PlayerNumber <= 2)
            {
                m_MovementAxisName = "Vertical" + m_PlayerNumber;
                m_TurnAxisName = "Horizontal" + m_PlayerNumber;
            }
            // Store the original pitch of the audio source.
            m_OriginalPitch = m_MovementAudio.pitch;
            m_moveDistance = 2;
            if (m_PlayerNumber == 1)
            {
                if (GameMode.Instance.currentMode == GameMode.mode.VSPLAYER)
                {
                    m_IsIA = false;
                    this.gameObject.GetComponent<BehaviorTreeManager>().enabled = false;
                    this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                }
                else
                {
                    m_IsIA = true;
                }
            }
            else
            {
                m_IsIA = true;
            }
            GetComponent<NavMeshAgent>().updatePosition = false;
        }

        public void MoveToTarget()
        {
            if (path != null)
            {
                if (this.GetComponent<Complete.TankShooting>().m_currCooldown <= 0)
                {
                    if(path.Count > 0)
                    //if(path.Count > 1)
                    {
                        this.transform.LookAt(pathNode[0].worldPosition);
                        //this.transform.LookAt((path[0] + path[1]) / 2);
                    }
                }
                else
                {
                    Turn(-1);
                }
            }
        }

        private void Update ()
        {
            if(m_PlayerNumber <= 2)
            {
                // Store the value of both input axes.
                m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
                m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
            }

            EngineAudio ();
        }

        private void EngineAudio ()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
            {
                // ... and if the audio source is currently playing the driving clip...
                if (m_MovementAudio.clip == m_EngineDriving)
                {
                    // ... change the clip to idling and play it.
                    m_MovementAudio.clip = m_EngineIdling;
                    m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play ();
                }
            }
            else
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (m_MovementAudio.clip == m_EngineIdling)
                {
                    // ... change the clip to driving and play.
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
        }


        private void FixedUpdate ()
        {

            if (!m_IsIA){
                //Move();
                MoveWithInput();
                Turn();
            }

        }

        public void SearchTarget()
        {
            currDistance = 1000;
            currDistance = Vector3.Distance(this.transform.position, PathManager.Instance.targetPoint.transform.position);
            this.GetComponent<Complete.TankShooting>().m_target = PathManager.Instance.targetPoint.transform;
            for (int i = 0; i < PathManager.Instance.allTanks.Length; i++)
            {
                if (Vector3.Distance(PathManager.Instance.allTanks[i].transform.position, this.transform.position) < currDistance || Vector3.Distance(PathManager.Instance.allTanks[i].transform.position, this.transform.position) < distanceShoot)
                {
                    if (this.gameObject != PathManager.Instance.allTanks[i])
                    {
                        currDistance = Vector3.Distance(PathManager.Instance.allTanks[i].transform.position, this.transform.position);
                        this.GetComponent<Complete.TankShooting>().m_target = PathManager.Instance.allTanks[i].transform;
                    }
                }
            }
        }

        public void IAMoveTo()
        {
            if (pathNode != null)
            {
                if (pathNode.Count > 0)
                {
                    if(this.GetComponent<Complete.TankShooting>().m_target != PathManager.Instance.targetPoint.transform)
                    {
                        if(currDistance >= 10)
                        {
                            this.transform.LookAt(pathNode[0].worldPosition);
                            Move();
                        }
                        else if (currDistance < 10 || this.GetComponent<Complete.TankShooting>().m_currCooldown >= 0)
                        {
                            Turn(this.transform.right.z);
                            Move();
                        }
                        else 
                        {
                            Turn(1);
                        }
                    }
                    else
                    {
                        this.transform.LookAt(pathNode[0].worldPosition);
                        Move();
                    }

                }
            }
        }
        
        private void Move ()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            //Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
            
            
            Vector3 movement;
            if (!m_IsIA)
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
            }
            else
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = transform.forward * m_Speed * Time.deltaTime;
            }
            
            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
        private void Move(bool _b)
        {

            Vector3 movement;
            if (_b)
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = transform.forward * m_Speed * Time.deltaTime;
            }
            else
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = -transform.forward * m_Speed * Time.deltaTime;
            }

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
        
        private void MoveWithInput()
        {
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        private void Turn ()
        {
            float turn;
            if (!m_IsIA)
            {
                // Determine the number of degrees to be turned based on the input, speed and time between frames.
                turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
            }
            else
            {
                turn = 1 * m_TurnSpeed * Time.deltaTime;
            }

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
        }
        private void Turn(float rotationRate)
        {
            float turn;
            if (!m_IsIA)
            {
                // Determine the number of degrees to be turned based on the input, speed and time between frames.
                turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
            }
            else
            {
                turn = -1 * m_TurnSpeed * Time.deltaTime;
            }

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        }

    }
}