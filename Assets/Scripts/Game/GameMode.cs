using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public enum mode { VSPLAYER,VSAI }

    public static GameMode Instance;
    public mode currentMode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    public void loadScene(bool gameMode)
    {
        if(gameMode == true)
        {
            currentMode = mode.VSPLAYER;
        }
        else
        {
            currentMode = mode.VSAI;
        }
        SceneManager.LoadScene("Assets/Tanks/_Complete-Game.unity", LoadSceneMode.Single);
    }
}
