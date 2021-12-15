using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomChanger : MonoBehaviour
{
    [SerializeField]
    private int roomId;

    //[SerializeField]
    //private bool isBossRoom;
    //[SerializeField]
    //private bool isTowerTestRoom;
    //[SerializeField]
    //private bool isWeaponTestRoom;
    //[SerializeField]
    //private bool isTrapTestRoom;
    //[SerializeField]
    //private bool isMainRoom;
    [SerializeField]
    RoomManager.RoomName room = RoomManager.RoomName.none;

    private bool isEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (room == RoomManager.RoomName.MainRoom ||
            room == RoomManager.RoomName.TrapRoom ||
            room == RoomManager.RoomName.TowerRoom ||
            room == RoomManager.RoomName.WeaponRoom)
        {
            if (collision.tag == "Player")
            {
                isEnter = true;
                if (roomId != RoomManager.Instance.RoomId)
                {
                    RoomManager.Instance.CurrentRoomName = room;
                    RoomManager.Instance.RoomId = roomId;
                }
            }
            return;
        }

        if (collision.tag == "Player" && !isEnter)
        {
            isEnter = true;
            RoomManager.Instance.RoomId = roomId;
           // RoomManager.Instance.IsBossRoom = isBossRoom;
        }

    }
   
}
