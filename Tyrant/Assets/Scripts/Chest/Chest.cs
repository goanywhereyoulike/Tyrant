using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer rend;
    [SerializeField]
    private Sprite openedSprite;

    private bool isOpen;

    virtual protected void OpenChest() { }

    private void Start()
    {
        isOpen = false;
        rend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOpen)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Interact(collision.gameObject.GetComponent<Player>());
            }
        }
    }
    public void Interact(Player player)
    {
        if (InputManager.Instance.GetKey("Interact"))
        {
            rend.sprite = openedSprite;
            OpenChest();
            isOpen = true;
        }
    }
}
