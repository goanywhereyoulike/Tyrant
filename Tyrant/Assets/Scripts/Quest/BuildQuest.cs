using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildQuest : Quest
{
    [SerializeField]
    private int targetTowerId;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        TowerManager.Instance.TowerBuilt += onTowerBuilt;
        base.Start();
    }
    private void onTowerBuilt(int towerId)
    {
        if (towerId == targetTowerId)
        {
            FinishQuest();
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
