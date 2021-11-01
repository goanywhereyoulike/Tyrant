using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUnlock : MonoBehaviour
{
    public bool isInfinite = false;
    public int index = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
           // ManagerSwitch mangerSwitch = FindObjectOfType<ManagerSwitch>();

            TrapManager TrapMngr = FindObjectOfType<TrapManager>();

            TrapMngr.AddTrap(index);

            if (!isInfinite)
            {
                gameObject.SetActive(false);
            }
           // Destroy(gameObject);

        }
    }
}
