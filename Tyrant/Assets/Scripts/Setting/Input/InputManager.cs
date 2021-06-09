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

    [SerializeField]
    private List<AxesLibrary> axesLibrary = null;

    private Dictionary<string, AxesLibrary> axesDict = null;
    private Dictionary<string, float> axesPositiveButtonValues = null;
    private Dictionary<string, float> axesNegativeButtonValues = null;

    private float epsilon = 0.0001f;

    public Vector3 MouseWorldPosition { get => Camera.main.ScreenToWorldPoint(Input.mousePosition); }

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

        axesDict = new Dictionary<string, AxesLibrary>();

        axesPositiveButtonValues = new Dictionary<string, float>();
        axesNegativeButtonValues = new Dictionary<string, float>();

        foreach (var axes in axesLibrary)
        {
            axesDict.Add(axes.name, axes);
            axesPositiveButtonValues.Add(axes.name, 0f);
            axesNegativeButtonValues.Add(axes.name, 0f);
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

    public float GetAxis(string axisName)
    {
        if (axesDict.ContainsKey(axisName))
        {
            axesPositiveButtonValues[axisName] -= axesDict[axisName].gravity * Time.deltaTime;
            if (Input.GetKey(axesDict[axisName].positiveButton))
            {
                axesPositiveButtonValues[axisName] += axesDict[axisName].step * Time.deltaTime;
            }

            if (axesPositiveButtonValues[axisName] <= epsilon)
            {
                axesPositiveButtonValues[axisName] = 0f;
            }

            axesPositiveButtonValues[axisName] = Mathf.Clamp(axesPositiveButtonValues[axisName], 0.0f, 1.0f);

            axesNegativeButtonValues[axisName] -= axesDict[axisName].gravity * Time.deltaTime;
            if (Input.GetKey(axesDict[axisName].negativeButton))
            {
                axesNegativeButtonValues[axisName] += axesDict[axisName].step * Time.deltaTime;
            }

            if (axesNegativeButtonValues[axisName] <= epsilon)
            {
                axesNegativeButtonValues[axisName] = 0f;
            }

            axesNegativeButtonValues[axisName] = Mathf.Clamp(axesNegativeButtonValues[axisName], 0.0f, 1.0f);

            float finalValue = axesPositiveButtonValues[axisName] - axesNegativeButtonValues[axisName];

            return finalValue;
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{axisName}' axis keys...");
        }

        return 0f;
    }

    public float GetAxisRaw(string axisName)
    {
        if (axesDict.ContainsKey(axisName))
        {
            float value = 0f;
            if (Input.GetKey(axesDict[axisName].positiveButton))
            {
                value += 1f;
            }

            if (Input.GetKey(axesDict[axisName].negativeButton))
            {
                value -= 1f;
            }

            return value;
        }
        else
        {
            Debug.LogError($"InputManager: Cannot find '{axisName}' axis keys...");
        }

        return 0f;
    }
}
