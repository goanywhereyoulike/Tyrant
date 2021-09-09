using UnityEngine;
using UnityEngine.UI;


public class DialogueActivate : MonoBehaviour,IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public Image tutorialButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            player.interactable = this;
            tutorialButton.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            if (player.interactable is DialogueActivate dialogueActive && dialogueActive == this)
            {
                player.interactable = null;
                tutorialButton.enabled = false;
            }
        }
    }
    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
