using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private string description;
    [SerializeField]
    private Text QuestDesText;
    [SerializeField]
    private Sprite QuestFinishedIcon;
    [SerializeField]
    private Sprite QuestUnifinishedIcon;
    [SerializeField]
    private Image QuestCheckBox;
    // Start is called before the first frame update
    void Start()
    {
        QuestDesText.text = description;
        QuestCheckBox.sprite = QuestUnifinishedIcon;
        
    }
    
    public void FinishQuest()
    {
        QuestCheckBox.sprite = QuestFinishedIcon;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
