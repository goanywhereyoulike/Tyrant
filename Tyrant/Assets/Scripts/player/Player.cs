using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour , IDamageable
{
    [SerializeField]
    private PlayerStates playerStates;

    [SerializeField]
    private Inventory myInventory = null;
    public Inventory MyInventory { get => myInventory; set => myInventory = value; }

    public Action healthChanged = null;

    private float health;
    public float Health
    {
        get => health;
        private set
        {
            health = value;
            healthChanged?.Invoke();
        }
    }

    private PlayerMovement playerMovement = null;

    [SerializeField]
    private PlayerUI playerUI = null;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStates.MoveSpeedChanged += () => playerMovement.MoveSpeed = playerStates.MoveSpeed;
        playerStates.MoveSpeedChanged?.Invoke();

        healthChanged += () => playerUI.HealthChanged(Health);
        healthChanged.Invoke();
        playerStates.MaxHealthChanged += () => playerUI.MaxHealthChanged(playerStates.MaxHealth);
        playerStates.MaxHealthChanged.Invoke();

        Health = playerStates.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("healthpotion"))
        {
            HealthPotion healthPotion = MyInventory.GetPickUp("health potion") as HealthPotion;
            if (healthPotion)
            {
                Health += healthPotion.AddHealth;
                MyInventory.DeletePickUp(healthPotion);
            }
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        if (InputManager.Instance.GetKeyDown("drop"))
        {
            MyInventory.DropPickUp(transform.position, MyInventory.GetPickUp("health potion"));
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
    public void HealthRecover(float recover)
    {
        Health += recover;
        if (Health > playerStates.MaxHealth)
        {
            Health = playerStates.MaxHealth;
        }

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        TakeDamage(5.0f);
    //        Debug.Log(health);
    //    }
    //}
}
