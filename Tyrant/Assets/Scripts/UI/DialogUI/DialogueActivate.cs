using UnityEngine;
using UnityEngine.UI;


public class DialogueActivate : MonoBehaviour,IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public Image tutorialButton;
    private bool isTriggered = false;
    private bool canTrigger;
    private Player player;
    private void Start()
    {
        isTriggered = false;
    }
    private void Update()
    {
        if (canTrigger && InputManager.Instance.GetKeyDown("Interact"))
        { 
            Time.timeScale = 0;
            Interact(player);
            isTriggered = true;
            canTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out Player players))
            {
                player = players;
                tutorialButton.enabled = true;
                canTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player players))
        {
            //player = null;
            tutorialButton.enabled = false;
            canTrigger = false;
        }
    }
    
    public void Interact(Player player)
    {
        isTriggered = true;
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
   
     
}

