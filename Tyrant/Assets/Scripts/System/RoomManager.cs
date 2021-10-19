using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public System.Action<int> RoomChanged = null;

    private int roomId;

    //public cameracontroller camera;
    private bool isBossRoom;
    public GameObject[] FogOfWar;
    public GameObject[] Pointers;
    private bool PointerEnabled;
    private SpawnManager spawnManager;
    private static RoomManager instance = null;
    public static RoomManager Instance { get => instance; }
    public int RoomId
    {
        get => roomId;
        set
        {
            roomId = value;
            RoomChanged?.Invoke(roomId);
        }
    }
    public bool IsBossRoom { get => isBossRoom; set => isBossRoom = value; }
    private bool Isallclear;

    public List<Door> Doors = new List<Door>();
    // Start is called before the first frame update
    void Awake()
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
        spawnManager = SpawnManager.Instance;
    }

    private void Update()
    {
        //for (int i = 0; i < Doors.Count; ++i)
        //{
        //    if (Doors[i].roomID <= RoomId && SpawnManager.Instance.RoomClear)
        //    {
        //        Doors[i].Animator.SetBool("IsClose", true);
        //    }
        //    else if (RoomId != 0)
        //    {
        //        if (RoomId - 1 == Doors[i].roomID)
        //        {
        //            Doors[i].gameObject.SetActive(true);
        //            Doors[i].Animator.SetBool("IsClose", false);
        //        }
        //    }
        //}
        for (int i = 0; i < Doors.Count; ++i)
        {
            if (SpawnManager.Instance.RoomClear)
            {
                Doors[i].Animator.SetBool("IsClose", true);
                //Doors[i].gameObject.transform.position = Vector3.Lerp(Camera.main.transform.position, Doors[i].gameObject.transform.position, 2.0f * Time.deltaTime);
                FogOfWar[roomId].SetActive(false);
                /*if (!PointerEnabled)
                {
                    Pointers[roomId].SetActive(true);
                    PointerEnabled = true;
                }  */
                if (!Doors[i].IsBossDoor)
                {
                    Doors[i].Animator.SetBool("IsClose", true);
                }

                for (int r = 0; r < SpawnManager.Instance.rooms.Count; r++)
                {
                    if (!SpawnManager.Instance.rooms[r].isBossRoom)
                    {
                        if (SpawnManager.Instance.rooms[r].clear)
                        {
                            Isallclear = true;
                        }
                        else
                        {
                            Isallclear = false;
                            break;
                        }
                    }
                }

                if(Isallclear)
                {
                    if(Doors[i].IsBossDoor)
                    {
                        Doors[i].Animator.SetBool("IsClose", true);
                    }
                }
            }
            else if (RoomId != 0)
            { 
                Doors[i].gameObject.SetActive(true);
                
                if (RoomId - 1 == Doors[i].roomID)
                {
                   /* PointerEnabled = false;*/
                    Doors[i].gameObject.SetActive(true);
                    Doors[i].Animator.SetBool("IsClose", false);
                }
            }
        }
    }

}
