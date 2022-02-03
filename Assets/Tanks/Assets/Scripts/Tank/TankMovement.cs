using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float Speed = 12f;                 // How fast the tank moves forward and back.
        public float TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
        public AudioSource MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioClip EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip EngineDriving;           // Audio to play when the tank is moving.
        public float PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
        public bool IsIA = true;
        public float moveDistance = 5;
        public float respawnTime = 5;
        public SO_Team m_Team;

        private string MovementAxisName;          // The name of the input axis for moving forward and back.
        private string TurnAxisName;              // The name of the input axis for turning.
        private Rigidbody Rigidbody;              // Reference used to move the tank.
        public float MovementInputValue;         // The current value of the movement input.
        public float TurnInputValue;             // The current value of the turn input.
        private float OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] particleSystems; // References to all the particles systems used by the Tanks
        public List<Vector3> path;
        public bool b_onPoint;

        public TankMovementMode MovementMode = null;
        public BehaviourTree BehaviourTree = null;
        public float turnInputValue { get => TurnInputValue; }
        [SerializeField] private BTNode _entryNode;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            if (IsIA)
            {
                BehaviourTree = (BehaviourTree)BehaviourTree.Clone();
                BehaviourTree.Blackboard.tankMovement = this;
                BehaviourTree.Blackboard.tankTransform = transform;
            }
        }
        
        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            Rigidbody.isKinematic = false;

            // Also reset the input values.
            MovementInputValue = 0f;
            TurnInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; ++i)
            {
                particleSystems[i].Play();
            }
        }


        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for(int i = 0; i < particleSystems.Length; ++i)
            {
                particleSystems[i].Stop();
            }
        }


        private void Start()
        {
            // The axes names are based on player number.
            if(PlayerNumber <= 3)
            {
                MovementAxisName = "Vertical" + PlayerNumber;
                TurnAxisName = "Horizontal" + PlayerNumber;
            }
            // Store the original pitch of the audio source.
            OriginalPitch = MovementAudio.pitch;
            moveDistance = 2;
            if (PlayerNumber <= 3)
            {
                if (GameMode.Instance.currentMode == GameMode.mode.VSPLAYER)
                {
                    IsIA = false;
                    this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                }
                else
                {
                    IsIA = true;
                }
            }
            else
            {

                IsIA = true;
            }
            b_onPoint = false;
            GetComponent<NavMeshAgent>().updatePosition = false;
        }

        private void Update()
        {
            if(PlayerNumber <= 2)
            {
                // Store the value of both input axes.
                MovementInputValue = Input.GetAxis(MovementAxisName);
                TurnInputValue = Input.GetAxis(TurnAxisName);
            }

            if (IsIA)
            {
                if (BehaviourTree.IsRunning)
                {
                    BehaviourTree.EvaluateTree();
                }
            }
            EngineAudio();
        }
        
        public void MoveToTarget()
        {
            /*
            if (path != null)
            {
                if (this.GetComponent<Complete.TankShooting>().currCooldown <= 0)
                {
                    if(path.Count > 0)
                    //if(path.Count > 1)
                    {
                        if (!b_onPoint)
                        {
                            this.transform.LookAt(path[0]);
                            //this.transform.LookAt((path[0] + path[1]) / 2);
                        }
                        else
                        {
                            var lookVector = this.GetComponent<Complete.TankShooting>().target.transform.position;
                            this.transform.LookAt(lookVector);
                        }
                    }
                }
                else
                {
                    Turn(-1);
                }
            }
            */
        }

        private void EngineAudio()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs (MovementInputValue) < 0.1f && Mathf.Abs (TurnInputValue) < 0.1f)
            {
                // ... and if the audio source is currently playing the driving clip...
                if (MovementAudio.clip == EngineDriving)
                {
                    // ... change the clip to idling and play it.
                    MovementAudio.clip = EngineIdling;
                    MovementAudio.pitch = Random.Range (OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                    MovementAudio.Play();
                }
            }
            else
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (MovementAudio.clip == EngineIdling)
                {
                    // ... change the clip to driving and play.
                    MovementAudio.clip = EngineDriving;
                    MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                    MovementAudio.Play();
                }
            }
        }


        private void FixedUpdate()
        {
            MoveWithInput();
            TurnWithInput();
        }
        
        private void Move()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            //Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

            Vector3 movement;
            if (!IsIA)
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;
            }
            else
            {
                // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
                movement = transform.forward * Speed * Time.deltaTime;
            }
            
            // Apply this movement to the rigidbody's position.
            Rigidbody.MovePosition(Rigidbody.position + movement);

            MovementInputValue = 0;
        }

        private void MoveWithInput()
        {
            Vector3 movement = transform.forward * MovementInputValue * BehaviourTree.Blackboard.movementSpeed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            Rigidbody.MovePosition(Rigidbody.position + movement);
        }

        private void Turn()
        {
            float turn;
            if (!IsIA)
            {
                // Determine the number of degrees to be turned based on the input, speed and time between frames.
                turn = TurnInputValue * TurnSpeed * Time.deltaTime;
            }
            else
            {
                turn = 1 * TurnSpeed * Time.deltaTime;
            }

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            Rigidbody.MoveRotation (Rigidbody.rotation * turnRotation);
        }

        public void Rotate(Vector3 destination)
        {
            Vector3 direction = Vector3.Normalize(destination - transform.position);

            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            //Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(BehaviourTree.Blackboard.playerColor) + ">████████████</color> angle = " + angle);

            if (angle > 0)
                TurnInputValue = 1;
            else
                TurnInputValue = -1;
        }
        
        private void TurnWithInput()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);

            TurnInputValue = 0;
        }
    }
}