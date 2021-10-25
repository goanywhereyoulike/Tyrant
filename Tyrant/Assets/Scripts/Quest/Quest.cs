using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    protected string description;
    [SerializeField]
    protected Text QuestDesText;
    [SerializeField]
    protected Sprite QuestFinishedIcon;
    [SerializeField]
    protected Sprite QuestUnifinishedIcon;
    [SerializeField]
    protected Image QuestCheckBox;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        QuestDesText.text = description;
        QuestCheckBox.sprite = QuestUnifinishedIcon;
        
    }
    
    public void FinishQuest()
    {
        QuestCheckBox.sprite = QuestFinishedIcon;
        StartCoroutine(WaitBeforeDisable());
    }
    // Update is called once per frame
    IEnumerator WaitBeforeDisable()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);

    }
}
