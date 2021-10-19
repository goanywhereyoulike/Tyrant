using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour , IDamageable, GameObjectsLocator.IGameObjectRegister
{
    [SerializeField]
    private PlayerStates playerStates;

    [SerializeField] private DialogueUI dialogueUI;

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
    private bool isInvulnerbale = false;
    public bool IsInvulnerbale { get=>isInvulnerbale; set => isInvulnerbale=value; }
    private PlayerMovement playerMovement = null;

    public DialogueUI DialogueUI => dialogueUI;
   

    [SerializeField]
    private PlayerUI playerUI = null;

    [SerializeField]
    private Cannon bulletUI = null;

    public int coin;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStates.MoveSpeedChanged += () => playerMovement.MoveSpeed = playerStates.MoveSpeed;
        playerStates.MoveSpeedChanged?.Invoke();

        healthChanged += () => playerUI.HealthChanged(Health);
        healthChanged.Invoke();
        playerStates.MaxHealthChanged += () => playerUI.MaxHealthChanged(playerStates.MaxHealth);
        playerStates.MaxHealthChanged.Invoke();
        StartCoroutine(Coin());

        Health = playerStates.MaxHealth;

        RegisterToLocator();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (InputManager.Instance.GetKeyDown("healthpotion"))
        {
            HealthPotion healthPotion = MyInventory.GetPickUp("health potion") as HealthPotion;
            if (healthPotion)
            {
                Health += healthPotion.AddHealth;
                MyInventory.DeletePickUp(healthPotion);
            }
        }*/

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        /*if (InputManager.Instance.GetKeyDown("drop"))
        {
            MyInventory.DropPickUp(transform.position, MyInventory.GetPickUp("health potion"));
        }*/
    }

    IEnumerator Coin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            coin += 5;
        }
    }
    public void TakeDamage(float damage)
    {
        if (!isInvulnerbale)
        {
            Health -= damage;
            AudioManager.instance.PlaySFX(3);
            if (Health < 0)
            {
                Health = 0;
                AudioManager.instance.PlaySFX(4);
                SceneManager.LoadScene("GameOverScene");
            }
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

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Player>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Player>(this);
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
