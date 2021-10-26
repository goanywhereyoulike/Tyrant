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
    //private int moveQuestCount = 0;
    public static int moveQuestSetCount = 0;
    private bool moveComplete=false;
    private bool shootComplete=false;
    private bool buildComplete=false;
    void Start()
    {
        board.SetActive(true);
        animator = board.GetComponent<Animator>();
        moveQuestSetCount = MoveQuestSet.transform.childCount;

        RoomManager.Instance.RoomChanged += this.OnRoomChanged;
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



    }
    bool firstUpdate=false;
    void ActiveMoveQuest()
    {
        if(!moveComplete)
        {
            MoveQuestSet.SetActive(true);

        }
        if (moveQuestSetCount==0)
        {
            moveComplete = true;
            animator.SetTrigger("QuestEnd");
  
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
        firstUpdate = true;
        yield return new WaitForSeconds(3);
        obj.SetActive(false);

    }
    void OnRoomChanged(int id)
    {
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.MainRoom && !moveComplete)
        {
            animator.SetTrigger("QuestBegin");
        }
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.WeaponRoom && !shootComplete)
        {
            animator.SetTrigger("QuestBegin");
        }
    }
}
