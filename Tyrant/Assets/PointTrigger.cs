using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public GameObject[] Pointers;
    //public GameObject[] FogofWar;
    //private static PointTrigger instance = null;
    //public static PointTrigger Instance { get => instance; }
    protected virtual void Trigger2DEnter(Collider2D collision) { }
    //void Awake()
    //{
    //    if (instance == null)
    //    {
          
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Pointers[RoomManager.Instance.RoomId].SetActive(false);
        }
    }
}
