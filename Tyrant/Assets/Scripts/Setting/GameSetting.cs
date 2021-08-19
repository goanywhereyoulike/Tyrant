using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    private bool gamePause= false;

    public bool GamePause 
       { 
        get => gamePause;
        set {
                gamePause = value;
                if (gamePause)
                {
                    Time.timeScale =0.0f;
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
             }  
       }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (InputManager.Instance.GetKeyDown("GamePause"))
        {
            GamePause = !GamePause;
        }
    }
}
