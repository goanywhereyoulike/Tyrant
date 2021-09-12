using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private SpriteRenderer rend;
    [SerializeField]
    private Sprite openedSprite;
    [SerializeField] private Image tutorialButton;

    private bool isOpen;
    private bool CanOpen = false;
    virtual protected void OpenChest() { }

    private void Start()
    {
        isOpen = false;
        rend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Interact(collision.gameObject.GetComponent<Player>());
                tutorialButton.enabled = true;
                CanOpen = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tutorialButton.enabled = false;
            CanOpen = false;
        }
    }
    private void Update()
    {
        if(CanOpen&& InputManager.Instance.GetKey("Interact"))
        {
            rend.sprite = openedSprite;
            OpenChest();
            isOpen = true;
            CanOpen = false;
        }
    }
    
}
