using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapQuest : Quest
{
    [SerializeField]
    private int targetTrapId;

    // Start is called before the first frame update
    protected override void Start()
    {
        TrapManager.Instance.TrapBuilt += onTrapBuilt;
        base.Start();
    }
    private void onTrapBuilt(int trapId)
    {
        if (trapId == targetTrapId)
        {
            FinishQuest();
        }


    }
}
