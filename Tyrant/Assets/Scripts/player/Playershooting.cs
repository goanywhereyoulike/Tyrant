using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershooting : MonoBehaviour
{
    private Weapon weapon = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Inventory inventory = gameObject.GetComponent<Inventory>();
            weapon = inventory.GetPickUp("Weapon") as Weapon;

        }
        if (InputManager.Instance.GetKeyDown("Fire"))
        {
            if (weapon)
            {
                Vector2 playerHeading;
                playerHeading = (InputManager.Instance.MouseWorldPosition - gameObject.transform.position).normalized;
                weapon.Fire(playerHeading);
            }


        }
    }
}
