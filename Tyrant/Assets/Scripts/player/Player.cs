using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour , IDamageable
{
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private Slider healthBar;
    public Inventory mInventory { get; set; }
    public Weapon mWeapon { get; set; }

    void Awake()
    {
        mInventory = GetComponent<Inventory>();
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("healthpotion"))
        {
            HealthPotion healthPotion = mInventory.GetPickUp("health potion") as HealthPotion;
            if (healthPotion)
            {
                health += healthPotion.AddHealth;
                mInventory.DeletePickUp(healthPotion);
            }

        }
        Debug.Log(health);
        healthBar.value = health;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5.0f);
            Debug.Log(health);
        }
    }
}
