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

        keyCodeDict = new Dictionary<string, KeyCode>();

        foreach (var keyLib in keyCodeLibrary)
        {
            keyCodeDict.Add(keyLib.name, keyLib.keyCode);
        }
    }

    public bool GetKey(string keyName)
    {
        if (keyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKey(keyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }

    public bool GetKeyDown(string keyName)
    {
        if (keyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKeyDown(keyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }

    public bool GetKeyUp(string keyName)
    {
        if (keyCodeDict.ContainsKey(keyName))
        {
            return Input.GetKeyUp(keyCodeDict[keyName]);
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{keyName}' key...");
        }

        return false;
    }
}
