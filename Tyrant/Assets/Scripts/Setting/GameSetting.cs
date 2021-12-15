using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{

    private static GameSetting instance = null;
    public static GameSetting Instance { get => instance; }

    private bool gamePause = false;
    public bool GamePause
    {
        get => gamePause;
        set
        {
            gamePause = value;
            if (gamePause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    public bool isDemoMode = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        /*if (InputManager.Instance.GetKeyDown("GamePause"))
        {
            GamePause = !GamePause;
        }*/
    }
}
