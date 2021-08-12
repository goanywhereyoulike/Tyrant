using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomChanger : MonoBehaviour
{
    [SerializeField]
    private int roomId;
    private bool isEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isEnter)
        {
            isEnter = true;
            RoomManager.Instance.RoomId = roomId;
        }
    }
}
