using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntTowerColorChange : MonoBehaviour
{
    float duration = 1.5f;
    private float t = 0;

    void Update()
    {

        ColorChanger();



    }

    void ColorChanger()
    {

        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.Lerp(Color.blue, Color.red, t));

        if (t < 1)
        {
            t += Time.deltaTime / duration;
        }
        else
        {
            t = 0;
        }


    }
}
