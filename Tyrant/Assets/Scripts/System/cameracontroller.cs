using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform target;
    public float playerViewZone = 0.0f;
    public float playerCamera { get { return playerViewZone; } set { playerViewZone = value; }  }
    public CameraMoving cameraMoving = null;

    private void Awake()
    {
        playerViewZone = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (InputManager.Instance.GetKeyDown("CameraZoomIn"))
            playerViewZone -= 0.2f;
        else if (InputManager.Instance.GetKeyDown("CameraZoomOut"))
            playerViewZone += 0.2f;

        Camera.main.orthographicSize = playerViewZone;
        //for (int i = 0; i < RoomManager.Instance.Doors.Count; i++)
        //{
        //    if (RoomManager.Instance.Doors[i].roomID <= RoomManager.Instance.RoomId && SpawnManager.Instance.RoomClear)
        //    {
        //       Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, 
        //           new Vector3(RoomManager.Instance.Doors[i].gameObject.transform.position.x, 
        //           RoomManager.Instance.Doors[i].gameObject.transform.position.y,
        //           RoomManager.Instance.Doors[i].gameObject.transform.position.z), 2.0f * Time.deltaTime);
        //    }
        //    else
        //    {
        //        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        //    }
        //}
        if (cameraMoving.MovingForward || cameraMoving.MovingBack)
            return;

        Camera.main.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
