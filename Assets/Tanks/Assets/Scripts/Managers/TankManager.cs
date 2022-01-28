﻿using System;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class TankManager
    {
        // This class is to manage various settings on a tank.
        // It works with the GameManager class to control how the tanks behave
        // and whether or not players have control of their tank in the 
        // different phases of the game.

        public Color PlayerColor;                             // This is the color this tank will be tinted.
        public Transform SpawnPoint;                          // The position and direction the tank will have when it spawns.
        [HideInInspector] public int PlayerNumber;            // This specifies which player this the manager for.
        [HideInInspector] public string ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
        [HideInInspector] public GameObject Instance;         // A reference to the instance of the tank when it is created.
        [HideInInspector] public int Wins;                    // The number of wins this player has so far.
        

        private TankMovement Movement;                        // Reference to tank's movement script, used to disable and enable control.
        private TankShooting Shooting;                        // Reference to tank's shooting script, used to disable and enable control.
        private GameObject CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


        public void Setup()
        {
            // Get references to the components.
            Movement = Instance.GetComponent<TankMovement>();
            Shooting = Instance.GetComponent<TankShooting>();
            CanvasGameObject = Instance.GetComponentInChildren<Canvas>().gameObject;

            // Set the player numbers to be consistent across the scripts.
            Movement.PlayerNumber = PlayerNumber;
            Shooting.PlayerNumber = PlayerNumber;

            // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
            ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">PLAYER " + PlayerNumber + "</color>";

            // Get all of the renderers of the tank.
            MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer>();

            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = PlayerColor;
            }
        }


        // Used during the phases of the game where the player shouldn't be able to control their tank.
        public void DisableControl()
        {
            Movement.enabled = false;
            Shooting.enabled = false;

            CanvasGameObject.SetActive (false);
        }


        // Used during the phases of the game where the player should be able to control their tank.
        public void EnableControl()
        {
            Movement.enabled = true;
            Shooting.enabled = true;

            CanvasGameObject.SetActive (true);
        }


        // Used at the start of each round to put the tank into it's default state.
        public void Reset()
        {
            Instance.transform.position = SpawnPoint.position;
            Instance.transform.rotation = SpawnPoint.rotation;

            Instance.SetActive (false);
            Instance.SetActive (true);
        }
    }
}