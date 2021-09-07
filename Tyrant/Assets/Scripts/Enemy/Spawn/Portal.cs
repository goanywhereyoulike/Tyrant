using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    string waveinformation;
    public TMP_Text WaveInfo;
    public int waveCount = 0;
    public int currentWave = 0;

    public int index = -1;
    // Start is called before the first frame update
    void Start()
    {
        WaveInfo.text = "0/0";
  
    }

    // Update is called once per frame
    void Update()
    {
        waveinformation = currentWave + "/" + waveCount;
        WaveInfo.text = waveinformation.ToString();
    }
}
