using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;
    public static InputManager Instance { get => instance; }

    [SerializeField]
    private List<KeyCodeLibrary> keyCodeLibrary = null;

    private Dictionary<string, KeyCode> keyCodeDict = null;
    public Dictionary<string, KeyCode> KeyCodeDict { get => keyCodeDict; set => keyCodeDict = value; }


    private Dictionary<string, float> holdingTimeDict = null;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        KeyCodeDict = new Dictionary<string, KeyCode>();
        holdingTimeDict = new Dictionary<string, float>();

        foreach (var keyLib in keyCodeLibrary)
        {
            KeyCodeDict.Add(keyLib.name, keyLib.keyCode);
        }
    }

    public bool GetKey(string keyName)
    {
        if (KeyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKey(KeyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }

    public bool GetKey(string keyName, out float holdingTime)
    {
        if (KeyCodeDict.ContainsKey(keyName))
        {
            if (Input.GetKey(KeyCodeDict[keyName]))
            {
                if (holdingTimeDict.ContainsKey(keyName))
                {
                    holdingTimeDict[keyName] += Time.deltaTime;
                    holdingTime = holdingTimeDict[keyName];
                    return true;
                }
                else
                {
                    holdingTimeDict.Add(keyName, 0.0f);
                    holdingTimeDict[keyName] += Time.deltaTime;
                    holdingTime = holdingTimeDict[keyName];
                    return true;
                }
            }
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        if (holdingTimeDict.ContainsKey(keyName))
        {
            holdingTimeDict[keyName] = 0.0f;
        }
        holdingTime = 0.0f;

        return false;
    }

    public bool GetKeyDown(string keyName)
    {
        if (KeyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKeyDown(KeyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }

    public bool GetKeyUp(string keyName)
    {
        if (KeyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKeyUp(KeyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }
}
