using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform target;
    public float playerViewZone = 0.0f;
    public float playerCamera { get { return playerViewZone; } set { playerViewZone = value; }  }

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
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y,transform.position.z);
    }
}
