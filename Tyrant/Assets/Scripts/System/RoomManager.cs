using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public enum RoomName
    {
        MainRoom,
        BossRoom,
        TowerRoom,
        WeaponRoom,
        TrapRoom
    }

    public System.Action<int> RoomChanged = null;

    private int roomId;

    //public cameracontroller camera;
    [SerializeField]
    private bool isTutorial;

    //private bool isMainRoom;
    //private bool isTowerTestRoom;
    //private bool isWeaponTestRoom;
    //private bool isTrapTestRoom;
    
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

    public RoomName CurrentRoomName { get; set; }
    public bool IsBossRoom { get => isBossRoom; set => isBossRoom = value; }
    public bool IsTutorial { get => isTutorial; set => isTutorial = value; }

    //public bool IsTrapTestRoom { get => isTrapTestRoom; set => isTrapTestRoom = value; }
    //public bool IsWeaponTestRoom { get => isWeaponTestRoom; set => isWeaponTestRoom = value; }
    //public bool IsTowerTestRoom { get => isTowerTestRoom; set => isTowerTestRoom = value; }
    //public bool IsMainRoom { get => isMainRoom; set => isMainRoom = value; }

    private bool Isallclear;

    private bool isMain;

    private bool isFirstUpdate;

    public List<Door> Doors = new List<Door>();

    [System.Serializable]
    public class TestRooms
    {
        public List<GameObject> enemy;
    }

    public List<TestRooms> testRooms = new List<TestRooms>();

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
        //spawnManager = SpawnManager.Instance;

        
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
        if (!IsTutorial)
        {
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

                    if (Isallclear)
                    {
                        if (Doors[i].IsBossDoor)
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
        else // tutorial part
        {

            if (!isFirstUpdate)
            {
                for (int i = 0; i < testRooms.Count; i++)
                {
                    for (int j = 0; j < testRooms[i].enemy.Count; j++)
                    {
                        testRooms[i].enemy[j].SetActive(false);
                    }
                }
                isMain = true;
                isFirstUpdate = true;
            }

            if (CurrentRoomName == RoomName.MainRoom)
            {
                //clear all
                if (!isMain)
                {
                    for (int i = 0; i < testRooms.Count; i++)
                    {
                        for (int j = 0; j < testRooms[i].enemy.Count; j++)
                        {
                            testRooms[i].enemy[j].SetActive(false);
                            testRooms[i].enemy[j].GetComponent<TestEnemy>().Reuse();
                        }
                    }
                    isMain = true;
                }

            }
            if (CurrentRoomName == RoomName.TowerRoom)
            {
                //close other item only tower can be use
                if (isMain)
                {
                    for (int j = 0; j < testRooms[roomId - 1].enemy.Count; j++)
                    {
                        testRooms[roomId - 1].enemy[j].SetActive(true);
                    }
                }
                isMain = false;
            }
            if (CurrentRoomName == RoomName.TrapRoom)
            {
                //close other item only trap can be use
                if (isMain)
                {
                    for (int j = 0; j < testRooms[roomId - 1].enemy.Count; j++)
                    {
                        testRooms[roomId - 1].enemy[j].SetActive(true);
                    }
                }
                isMain = false;
            }
            if (CurrentRoomName == RoomName.WeaponRoom)
            {
                //close other item only weapon can be use
                if (isMain)
                {
                    for (int j = 0; j < testRooms[roomId - 1].enemy.Count; j++)
                    {
                        testRooms[roomId - 1].enemy[j].SetActive(true);
                    }
                }
                isMain = false;
            }
        }
    }

}
