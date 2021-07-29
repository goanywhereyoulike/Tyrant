using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public System.Action RoomChanger = null;

    private int roomId;

    private static RoomManager instance = null;
    public static RoomManager Instance { get => instance; }
    public int RoomId { get => roomId; set => roomId = value; }

    // Start is called before the first frame update
    void Start()
    {
        //Singleton pattern
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
