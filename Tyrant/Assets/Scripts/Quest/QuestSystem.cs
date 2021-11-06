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
    private GameObject BuildQuestSet;
    [SerializeField]
    private GameObject TrapQuestSet;
    [SerializeField]
    private GameObject board;
    private Animator animator;
    // Start is called before the first frame update
    //[SerializeField]
    private static QuestSystem instance = null;
    public static QuestSystem Instance { get => instance; }
    private GameObject currentQuestSet;
    public int CurrentQuestSetCount { get => currentQuestSetCount; 
        set 
        { 
            currentQuestSetCount = value; 
            if(currentQuestSetCount==0)
            {
                animator.SetBool("QuestEnd", true);
                if(currentQuestSet == MoveQuestSet)
                {
                    moveComplete = true;
                    StartCoroutine(WaitBeforeDisable(MoveQuestSet));
                }
                if(currentQuestSet == ShootQuestSet)
                {
                    shootComplete = true;
                    StartCoroutine(WaitBeforeDisable(ShootQuestSet));
                }
                if (currentQuestSet == BuildQuestSet)
                {
                    buildComplete = true;
                    StartCoroutine(WaitBeforeDisable(BuildQuestSet));
                }
                if (currentQuestSet == TrapQuestSet)
                {
                    trapComplete = true;
                    StartCoroutine(WaitBeforeDisable(TrapQuestSet));
                }
                currentQuestSet = null;
            }
        } 
    }

    public bool MoveComplete { get => moveComplete; }
    public bool ShootComplete { get => shootComplete; }
    public bool BuildComplete { get => buildComplete; }
    public bool TrapComplete { get => trapComplete; }

    private int currentQuestSetCount = 0;
    //public  int moveQuestSetCount;
    private bool moveComplete=false;
    private bool shootComplete=false;
    private bool buildComplete=false;
    private bool trapComplete = false;
    //private bool firstRun = true;
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
        if(!MoveComplete)
        {
            MoveQuestSet.SetActive(true);
            CurrentQuestSetCount = MoveQuestSet.transform.childCount;
            currentQuestSet = MoveQuestSet;
        }
        
    }
    void ActiveShootQuest()
    {
        if (!ShootComplete)
        {
            ShootQuestSet.SetActive(true);
            CurrentQuestSetCount = ShootQuestSet.transform.childCount;
            currentQuestSet = ShootQuestSet;
        }
      
    }
    void ActiveBuildQuest()
    {
        if(!BuildComplete)
        {
            BuildQuestSet.SetActive(true);
            CurrentQuestSetCount = BuildQuestSet.transform.childCount;
            currentQuestSet = BuildQuestSet;
        }
    }
     void ActiveTrapQuest()
    {
        if(!TrapComplete)
        {
            TrapQuestSet.SetActive(true);
            CurrentQuestSetCount = TrapQuestSet.transform.childCount;
            currentQuestSet = TrapQuestSet;
        }
    }
    IEnumerator WaitBeforeDisable(GameObject obj)
    {
        //firstUpdate = true;
        yield return new WaitForSeconds(1);
        obj.SetActive(false);

    }
    void OnRoomChanged(int id)
    {
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.MainRoom && !MoveComplete && currentQuestSet == null)
        {
            ActiveMoveQuest();
            animator.SetTrigger("QuestBegin");
        }
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.WeaponRoom && !ShootComplete && currentQuestSet == null)
        {
            ActiveShootQuest();
            animator.SetTrigger("QuestBegin");
        }
        if (RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.TowerRoom && !BuildComplete && currentQuestSet == null)
        {
            ActiveBuildQuest();
            animator.SetTrigger("QuestBegin");
        }
        if (RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.TrapRoom && !TrapComplete && currentQuestSet == null)
        {
            ActiveTrapQuest();
            animator.SetTrigger("QuestBegin");
        }
    }

}
