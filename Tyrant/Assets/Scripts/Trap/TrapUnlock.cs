using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUnlock : MonoBehaviour
{
    private int index = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            ManagerSwitch mangerSwitch = FindObjectOfType<ManagerSwitch>();

            TrapManager TrapMngr = mangerSwitch.GetComponentInChildren<TrapManager>(true);
            index = Random.Range(0, 3);
            Debug.Log(index);
            if (index == 0)
            {
                TrapMngr.AddClipTrap();
            }
            else if (index == 1)
            {
                TrapMngr.AddBombTrap();
            }
            else if (index == 2)
            {
                TrapMngr.AddBlackHoleTrap();
            }


            gameObject.SetActive(false);
           // Destroy(gameObject);

        }
    }
}
