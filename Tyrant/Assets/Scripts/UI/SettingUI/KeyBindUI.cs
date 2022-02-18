using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindUI : MonoBehaviour
{
    [SerializeField]
    public Text keyName;
    [SerializeField]
    public Button keycode;
    private KeyCode kCode;

    private void Update()
    {
        if (InputManager.Instance.GetKeyDown(keyName.text))
        {
            Debug.Log("hello");
        }
    }
    public void OnButtonClicked()
    {
        keycode.GetComponentInChildren<Text>().text = "Press a key to bind";
        StartCoroutine(DetectInput());

        //keycode.GetComponentInChildren<Text>().text = kCode.ToString();

    }
    IEnumerator DetectInput()
    {
        bool done = false;
        //while (!done)
        //{
        //    yield return null;

        //    if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse0))
        //    {
        //        kCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString, true);
        //        print(kCode);
        //        done = true;

        //    }

        //}
        while(!done)
        {
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Mouse0))
            {
                kCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString, true);
                done = true;
            }

            yield return null;
        }
        
        keycode.GetComponentInChildren<Text>().text = kCode.ToString();
        InputManager.Instance.SetKey(keyName.text, kCode);
        print(InputManager.Instance.KeyCodeDict[keyName.text]);

    }

    IEnumerator waitForKeyPressed()
    {
        yield return new WaitUntil(() => kCode != KeyCode.None);
    }
}
