using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUI : MonoBehaviour
{
    [SerializeField]
    private Text Spawntext;

    private int maxSpawn;
    private int spawncount;
   
    public void MaxSpawn(int value)
    {
        maxSpawn = value;
    }
    public void SpawnChanged(int value)
    {
        spawncount = value;
    }

    private void Update()
    {
        Spawntext.text = spawncount.ToString() + " / " + maxSpawn.ToString();
    }
}
