using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTest : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {

            //TrapManager instance;
            //var all = Resources.FindObjectsOfTypeAll<TrapManager>();
            //if (all.Length > 0)
            //{
            //    instance = all[0];
            //} 
            ManagerSwitch mangerSwitch = FindObjectOfType<ManagerSwitch>();

            TrapManager TrapMngr = mangerSwitch.GetComponentInChildren<TrapManager>(true);

            TrapMngr.AddClipTrap();
            TrapMngr.AddBombTrap();
            TrapMngr.AddBlackHoleTrap();
            Destroy(gameObject);


        }
    }



}
