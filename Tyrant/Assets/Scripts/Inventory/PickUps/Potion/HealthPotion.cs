using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUp
{
    [SerializeField]
    private int addHealth;
    private Player player;
    public int AddHealth { get => addHealth; set => addHealth = value; }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ObjectPoolManager.Instance.InstantiateObjects("health potion");
    }
    private void Update()
    {
        if (InputManager.Instance.GetKeyDown("pick") && CanBePicked)
        {
            player.mInventory.AddPickUp(this);
            gameObject.SetActive(false);
        }
    }
}
