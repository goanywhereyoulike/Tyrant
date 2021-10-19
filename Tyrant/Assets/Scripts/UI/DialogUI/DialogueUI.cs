using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;//test object
    public bool isOpen { get; set; }
    private TextEffect textEffect;

    private void Start()
    {
        textEffect = GetComponent<TextEffect>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);

    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(stepThroughDialogue(dialogueObject));
    }
    private IEnumerator stepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return runTextEffect(dialogue);
            textLabel.text = dialogue;
            yield return new WaitUntil(() => InputManager.Instance.GetKeyDown("NextDialogue"));
        }
        Time.timeScale = 1;
        CloseDialogueBox();
    }

    private IEnumerator runTextEffect(string dialogue)
    {
        textEffect.Run(dialogue, textLabel);
        while (textEffect.IsRunning)
        {
            yield return null;
            if (InputManager.Instance.GetKeyDown("Interact"))
            {
                textEffect.Stop();
            }
            if (InputManager.Instance.GetKeyDown("SkipDialogue"))
            {
                textEffect.Stop();
            }
        }
    }
    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
