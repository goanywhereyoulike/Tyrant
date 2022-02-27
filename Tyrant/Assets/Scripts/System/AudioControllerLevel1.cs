using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("Level_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
