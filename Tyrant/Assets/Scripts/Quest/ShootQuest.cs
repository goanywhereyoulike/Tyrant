using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootQuest : Quest
{
    [SerializeField]
    private Weapon targetWeapon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (targetWeapon != null)
        {
            targetWeapon.Fired += this.onWeaponFired;
        }
    }
    private void onWeaponFired()
    {
        FinishQuest();
        targetWeapon.Fired-= this.onWeaponFired;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
