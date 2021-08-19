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
            yield return textEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => InputManager.Instance.GetKeyDown("NextDialogue"));
        }
        CloseDialogueBox();
    }
    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
