using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "ScriptableObjects/Tank/Team")]
public class SO_Team : ScriptableObject
{
    public int m_TeamNumber;
    public float m_TeamScore;

    void Start()
    {
        m_TeamScore = 0; 
    }
}
