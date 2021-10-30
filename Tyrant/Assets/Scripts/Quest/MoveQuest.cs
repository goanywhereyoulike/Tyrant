using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveQuest : Quest
{
    [SerializeField]
    private string key;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown(key))
        {
            FinishQuest();
        }
    }
  
}
