using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercombat : MonoBehaviour
{
    private Gun gun = null;
    public Transform meleePoint;
    public float meleeRange = 1.0f;
    public float meleeSpeed = 2.0f;
    public Inventory mInventory { get; set; }
    float timeBtwAttack = 0f;
    public LayerMask enemyLayers;
    public GameObject WeaponPosition;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Time.time >= timeBtwAttack)
        {
            if (InputManager.Instance.GetKeyDown("Melee"))
            {
                meleeAttack();
                timeBtwAttack = Time.time + 1f / meleeSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Inventory inventory = gameObject.GetComponent<Inventory>();
            gun = inventory.GetPickUp("Weapon") as Gun;

            gun.transform.position = WeaponPosition.transform.position;
            gun.transform.parent = WeaponPosition.transform;
            gun.gameObject.SetActive(true);


        }

        //if (InputManager.Instance.GetKeyDown("drop"))
        //{
        //    mInventory.DropPickUp(transform.position, mInventory.GetPickUp("gun"));
        //}
        Vector2 playerHeading;
        playerHeading = (InputManager.Instance.MouseWorldPosition - gameObject.transform.position).normalized;
        float weaponAngle = Mathf.Atan2(playerHeading.y, playerHeading.x) * Mathf.Rad2Deg;
        gun.transform.eulerAngles = new Vector3(0.0f, 0.0f, weaponAngle);
        if (InputManager.Instance.GetKeyDown("Fire"))
        {
            if (gun)
            {
                gun.Fire(playerHeading);
            }
        }
      

    }
    void meleeAttack()
    {
        Collider2D[] meleeHits = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange,enemyLayers);
        foreach (Collider2D enemy in meleeHits)
        {
            Debug.Log("hit" + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(meleePoint ==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
