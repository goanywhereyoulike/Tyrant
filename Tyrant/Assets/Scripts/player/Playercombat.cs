using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercombat : MonoBehaviour
{
    private Gun gun = null;
    public Transform meleePoint;
    public Transform firePosition;
    GameObject Playerbullet;
    
    public GameObject BulletPrefab;
    public float meleeRange = 1.0f;
    public float meleeSpeed = 2.0f;
    public Inventory mInventory { get; set; }
    float timeBtwAttack = 0f;
    LayerMask enemyLayers;

  
    void Update()
    {
       

        if(Time.time>= timeBtwAttack)
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

        }

        //if (InputManager.Instance.GetKeyDown("drop"))
        //{
        //    mInventory.DropPickUp(transform.position, mInventory.GetPickUp("gun"));
        //}
       
        if (InputManager.Instance.GetKeyDown("Fire"))
        {
            
            if (gun)
            {
                Vector2 playerHeading;
                Vector2 mousePosition = new Vector2(InputManager.Instance.MouseWorldPosition.x, InputManager.Instance.MouseWorldPosition.y);
                Vector2 selfPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                playerHeading = (mousePosition-selfPosition).normalized;
                Fire(playerHeading);
            }
        }

    }


    public void Fire(Vector2 playerHeading)
    {
        
        Playerbullet = ObjectPoolManager.Instance.GetPooledObject("bullet");    
        if (Playerbullet)
        {
            Playerbullet.SetActive(true);
            Playerbullet.transform.position = firePosition.position;
            Playerbullet.GetComponent<bullet>().Direction = playerHeading;
            Playerbullet.GetComponent<bullet>().startPosition = firePosition.position;
        }

        //Instantiate(bullet, firePosition.position, firePosition.rotation);
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
