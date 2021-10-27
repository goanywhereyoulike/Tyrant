using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject MoveQuestSet;
    [SerializeField]
    private GameObject ShootQuestSet;
    [SerializeField]
    private GameObject board;
    private Animator animator;
    // Start is called before the first frame update
    //[SerializeField]
    private static QuestSystem instance = null;
    public static QuestSystem Instance { get => instance; }
    public int CurrentQuestSetCount { get => currentQuestSetCount; 
        set 
        { 
            currentQuestSetCount = value; 
            if(currentQuestSetCount==0)
            {

            }
        } 
    }

    private int currentQuestSetCount = 0;
    //public  int moveQuestSetCount;
    private bool moveComplete=false;
    private bool shootComplete=false;
    private bool buildComplete=false;
    private bool firstRun = true;
    /*public  int MoveQuestSetCount
    { 
        get => moveQuestSetCount; 
        set 
        { 
            moveQuestSetCount = value;

        } 
    }*/

    void Start()
    {
        if(instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        board.SetActive(true);
        animator = board.GetComponent<Animator>();
        
        RoomManager.Instance.RoomChanged += this.OnRoomChanged;
        ActiveMoveQuest();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.MainRoom)
        {
            ActiveMoveQuest();
        }
        if (RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.WeaponRoom)
        {
            ActiveShootQuest();
        }*/
        /*if (MoveQuestSetCount == 0 && firstRun)
        {
            moveComplete = true;
            animator.SetBool("QuestEnd",true);
            MoveQuestSet.SetActive(false);
            firstRun = false;
        }*/


    }
    //bool firstUpdate=false;
    void ActiveMoveQuest()
    {
        if(!moveComplete)
        {
            MoveQuestSet.SetActive(true);
            CurrentQuestSetCount = MoveQuestSet.transform.childCount;


        }
        
    }
    void ActiveShootQuest()
    {
        if (!shootComplete)
        {
            ShootQuestSet.SetActive(true);
        }
        if (board.transform.childCount == 0)
        {
            shootComplete = true;
        }
    }
    void ActiveBuildQuest()
    {

    }

    IEnumerator WaitBeforeDisable(GameObject obj)
    {
        //firstUpdate = true;
        yield return new WaitForSeconds(3);
        obj.SetActive(false);

    }
    void OnRoomChanged(int id)
    {
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.MainRoom && !moveComplete)
        {
            ActiveMoveQuest();
            animator.SetTrigger("QuestBegin");
        }
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.WeaponRoom && !shootComplete)
        {
            ActiveShootQuest();
            animator.SetTrigger("QuestBegin");
        }
    }
    void onQuestCompleted()
    {

    }
}
