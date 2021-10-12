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
    }

    private void Update()
    {
        for (int i = 0; i < Doors.Count; ++i)
        {
            if (Doors[i].roomID <= RoomId && SpawnManager.Instance.RoomClear)
            {
                Doors[i].Animator.SetBool("IsClose", true);
                //Doors[i].gameObject.transform.position = Vector3.Lerp(Camera.main.transform.position, Doors[i].gameObject.transform.position, 2.0f * Time.deltaTime);
                //FogOfWar[roomId].SetActive(false);
                Image image = FogOfWar[roomId].gameObject.GetComponent<Image>();
                while (image.color.a != 0.0f)
                {
                    image.color -= new Color(0.0f, 0.0f, 0.0f, 0.1f);
                }
            }
            else if (RoomId != 0)
            {
                if (RoomId - 1 == Doors[i].roomID)
                {
                    Doors[i].gameObject.SetActive(true);
                    Doors[i].Animator.SetBool("IsClose", false);
                }
            }
        }
    }

}
