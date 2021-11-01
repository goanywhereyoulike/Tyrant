using System;
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
        QuestSystem.Instance.CurrentQuestSetCount--;
        //StartCoroutine(WaitBeforeDisable());
    }
    // Update is called once per frame
    
}