using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject MoveQuestSet;
    [SerializeField]
    private GameObject ShootQuestSet;
    [SerializeField]
    private GameObject board;
    // Start is called before the first frame update
    private bool moveComplete;
    private bool shootComplete;
    private bool buildComplete;
    void Start()
    {
        board.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.MainRoom)
        {
            ActiveMoveQuest();
        }
        if (RoomManager.Instance.CurrentRoomName == RoomManager.RoomName.WeaponRoom)
        {
            ActiveShootQuest();
        }



    }
    void ActiveMoveQuest()
    {
        if(!moveComplete)
        {
            MoveQuestSet.SetActive(true);
        }
        if(board.transform.childCount==0)
        {
            moveComplete = true;
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
}
