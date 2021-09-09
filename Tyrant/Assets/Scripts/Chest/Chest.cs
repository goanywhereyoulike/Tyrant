using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer rend;
    [SerializeField]
    private Sprite openedSprite;
    [SerializeField] private Image tutorialButton;

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
                tutorialButton.enabled = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tutorialButton.enabled = false;
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
