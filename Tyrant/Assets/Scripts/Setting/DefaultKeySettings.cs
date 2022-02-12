using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DefaultKeySettings : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        Color color = Color.white;
        color.a = 0;
        image.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("DefaultKeySetttings"))
        {
            Time.timeScale = 0;
            Color color = Color.white;
            color.a = 200;
            image.color = color;
        }

        if (InputManager.Instance.GetKeyUp("DefaultKeySetttings"))
        {
            Time.timeScale = 1;
            Color color = Color.white;
            color.a = 0;
            image.color = color;

        }
    }
}
