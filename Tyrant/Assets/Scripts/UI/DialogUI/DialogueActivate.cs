using UnityEngine;
using UnityEngine.UI;


public class DialogueActivate : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private bool isTriggered = false;
    private bool canTrigger;
    private Player player;
    private bool startChecked;
    [SerializeField]
    private bool autoPlayOnce;
    private void Start()
    {
        isTriggered = false;
    }
    private void Update()
    {
        if ((canTrigger && InputManager.Instance.GetKeyDown("Interact")) || (canTrigger && autoPlayOnce))
        {
            Time.timeScale = 0;
            Interact(player);
            isTriggered = true;
            canTrigger = false;
            autoPlayOnce = false;
            IndicatorSystem.DisableIndicator(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player players))
        {
            player = players;
            player.InteractButton.GetComponent<Image>().enabled = true;
            canTrigger = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player players))
        {
            //player = null;
            player.InteractButton.GetComponent<Image>().enabled = false;
            canTrigger = false;
        }
    }

    public void Interact(Player player)
    {
        isTriggered = true;
        player.DialogueUI.ShowDialogue(dialogueObject);
        if (!startChecked)
        {
            //SpawnManager.Instance.StartLevel = true;
            startChecked = true;
        }
    }


}

