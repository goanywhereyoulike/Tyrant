using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Pointers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Pointers[RoomManager.Instance.RoomId].SetActive(false);
        }
    }
}
