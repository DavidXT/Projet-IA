using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
        public GameObject m_TankPrefab;             // Reference to the prefab the players will control.
        public TankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks.
        public float Timer;
        public Text timerText;
        public bool b_isPlaying = false;
        public SO_Team[] m_Teams;

        public GameObject m_hellipad;

        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.         


        const float k_MaxDepenetrationVelocity = float.PositiveInfinity;

        
        private void Start()
        {
            // This line fixes a change to the physics engine.
            Physics.defaultMaxDepenetrationVelocity = k_MaxDepenetrationVelocity;
            
            // Create the delays so they only have to be made once.
            m_StartWait = new WaitForSeconds (m_StartDelay);
            m_EndWait = new WaitForSeconds (m_EndDelay);

            SpawnAllTanks();
            SetCameraTargets();

            // Once the tanks have been created and the camera is using them as targets, start the game.
            StartCoroutine (GameLoop ());
        }

        private void Update()
        {
            if (b_isPlaying)
            {
                if (Timer > 0)
                {
                    Timer -= Time.deltaTime;
                    timerText.text = System.Math.Round(Timer, 0).ToString();
                    TimerEnd();
                }
                RespawnTanks();
            }

        }

        private void RespawnTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {

                // ... and if one of them is active, it is the winner so return it.
                if (!m_Tanks[i].m_Instance.activeSelf)
                {
                    TankMovement tank = m_Tanks[i].m_Instance.GetComponent<Complete.TankMovement>();
                    if(tank.respawnTime > 0)
                    {
                        tank.respawnTime -= Time.deltaTime;
                    }
                    else
                    {
                        m_Tanks[i].m_Instance.transform.position = m_Tanks[i].m_SpawnPoint.position;
                        m_Tanks[i].m_Instance.SetActive(true);
                        tank.respawnTime = 5;
                    }
                }
                    
            }
        }
        private void SpawnAllTanks()
        {
            // For all the tanks...
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // ... create them, set their player number and references needed for control.

                m_Tanks[i].m_Instance =
                    Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
                m_Tanks[i].m_PlayerNumber = i + 1;
                m_Tanks[i].Setup();
            }
        }


        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of tanks.
            Transform[] targets = new Transform[m_Tanks.Length];

            // For each of these transforms...
            for (int i = 0; i < targets.Length; i++)
            {
                // ... set it to the appropriate tank transform.
                targets[i] = m_Tanks[i].m_Instance.transform;
            }

            // These are the targets the camera should follow.
            m_CameraControl.Targets = targets;
        }


        // This is called from start and will run each phase of the game one after another.
        private IEnumerator GameLoop ()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine (RoundStarting ());

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine (RoundPlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            yield return StartCoroutine (RoundEnding());

        }


        private IEnumerator RoundStarting ()
        {
            // As soon as the round starts reset the tanks and make sure they can't move.
            ResetAllTanks ();
            DisableTankControl ();
            b_isPlaying = false;
            // Snap the camera's zoom and position to something appropriate for the reset tanks.
            m_CameraControl.SetStartPositionAndSize ();

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying ()
        {
            // As soon as the round begins playing let the players control the tanks.
            EnableTankControl ();
            b_isPlaying = true;
            // Clear the text from the screen.
            m_MessageText.text = string.Empty;

            // While there is not one tank left...
            while (!TimerEnd())
            {
                // ... return on the next frame.
                yield return null;
            }
        }

        private IEnumerator RoundEnding ()
        {
            // Stop tanks from moving.
            DisableTankControl ();
            Time.timeScale = 0;
            b_isPlaying = false;

            // Get a message based on the scores and whether or not there is a game winner and display it.
            string message = EndMessage ();
            m_MessageText.text = message;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_EndWait;
            SceneManager.LoadScene(0);
        }

        private bool TimerEnd()
        {
            return Timer <= 0;
        }


        // Returns a string message to display at the end of each round.
        private string EndMessage()
        {
            // By default when a round ends there are no winners so the default end message is a draw.
            string message = "GAME END!";

            // Add some line breaks after the initial message.
            message += "\n\n\n";

            int winner = 0;
            float winnerScore = 0;
            // Go through all the tanks and add each of their scores to the message.
            for (int i = 0; i < m_Teams.Length; i++)
            {
                message += "Team  " + m_Teams[i].m_TeamNumber + ": " + System.Math.Round(m_Teams[i].m_TeamScore) + " PTS\n";
                if(m_Teams[i].m_TeamScore > winnerScore)
                {
                    winnerScore = m_Teams[i].m_TeamNumber;
                    winner = m_Teams[i].m_TeamNumber;
                }
            }

            // Add some line breaks after the initial message.
            message += "\n\n\n";
            message += "Team " + winner + " WINS THE GAME!";

            return message;
        }


        // This function is used to turn all the tanks back on and reset their positions and properties.
        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].Reset();
            }
        }


        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].EnableControl();
            }
        }


        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
        }
    }
}