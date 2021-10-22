using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // Singleton
    private static CameraMoving instance = null;
    public static CameraMoving Instance { get => instance; }
    public List<Vector3> LerpPos { get => lerpPos; set => lerpPos = value; }

    [SerializeField] private float cameraMovingSpeed = 0.0f;
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

    [SerializeField] private List<Vector3> lerpPos = null;

    public bool MovingForward { get; set; }
    public bool MovingBack { get; set; }

    private void Update()
    {
        if (lerpPos.Count == 0)
            return;

        if (SpawnManager.Instance.RoomClear)
        {
            MovingForward = true;
        }

        if (MovingForward)
        {
            float delta = cameraMovingSpeed * Time.deltaTime;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, lerpPos[0], delta);
        }

        if ((Camera.main.transform.position - lerpPos[0]).magnitude <= 0.00001f)
        {
            lerpPos.RemoveAt(0);
            MovingForward = false;
            MovingBack = false;
        }
    }
}
