using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float playerViewZone = 0.0f;
    public float playerCamera { get { return PlayerViewZone; } set { PlayerViewZone = value; }  }
    //public CameraMoving cameraMoving = null;

    public Vector2 LeftTopWorldPos { get { return Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)); } }
    public Vector2 RightTopWorldPos { get { return Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)); } }
    public Vector2 LeftBottomWorldPos { get { return Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)); } }
    public Vector2 RightBottomWorldPos { get { return Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)); } }

    [SerializeField]
    private EdgeCollider2D camBox = null;

    public float PlayerViewZone
    {
        get => playerViewZone;
        set
        {
            playerViewZone = value;
            Camera.main.orthographicSize = PlayerViewZone;

            Vector2[] colliderPoints = new Vector2[5];

            colliderPoints[0] = LeftTopWorldPos - new Vector2(transform.position.x, transform.position.y);
            colliderPoints[1] = LeftBottomWorldPos - new Vector2(transform.position.x, transform.position.y);
            colliderPoints[2] = RightBottomWorldPos - new Vector2(transform.position.x, transform.position.y);
            colliderPoints[3] = RightTopWorldPos - new Vector2(transform.position.x, transform.position.y);
            colliderPoints[4] = LeftTopWorldPos - new Vector2(transform.position.x, transform.position.y);

            camBox.points = colliderPoints;
        }
    }

    private void Awake()
    {
        PlayerViewZone = Camera.main.orthographicSize;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (InputManager.Instance.GetKeyDown("CameraZoomIn"))
            PlayerViewZone -= 0.2f;
        else if (InputManager.Instance.GetKeyDown("CameraZoomOut"))
            PlayerViewZone += 0.2f;

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
        //if (cameraMoving.MovingForward || cameraMoving.MovingBack)
        //    return;

        Camera.main.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -20);
    }
}
