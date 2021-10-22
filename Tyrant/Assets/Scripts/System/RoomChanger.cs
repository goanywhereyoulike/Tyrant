using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomChanger : MonoBehaviour
{
    [SerializeField]
    private int roomId;
    [SerializeField]
    private bool isBossRoom;
    [SerializeField]
    private bool isTowerTestRoom;
    [SerializeField]
    private bool isWeaponTestRoom;
    [SerializeField]
    private bool isTrapTestRoom;
    [SerializeField]
    private bool isMainRoom;

    private bool isEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMainRoom || isTrapTestRoom|| isTowerTestRoom || isWeaponTestRoom)
        {
            if (collision.tag == "Player")
            {
                isEnter = true;
                RoomManager.Instance.RoomId = roomId;
                RoomManager.Instance.IsBossRoom = isBossRoom;
                RoomManager.Instance.IsWeaponTestRoom = isWeaponTestRoom;
                RoomManager.Instance.IsTowerTestRoom = isTowerTestRoom;
                RoomManager.Instance.IsTrapTestRoom = isTrapTestRoom;
                RoomManager.Instance.IsMainRoom = isMainRoom;
            }
            return;
        }

        if (collision.tag == "Player" && !isEnter)
        {
            isEnter = true;
            RoomManager.Instance.RoomId = roomId;
            RoomManager.Instance.IsBossRoom = isBossRoom;
        }
        
    }
}
