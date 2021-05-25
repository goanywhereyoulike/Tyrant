using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{

    RaycastHit2D hit;
    Vector2 movePoint;
    PlayerMovement player;
    public GameObject prefab;
    public Vector3 offset;
    bool followplayer = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 followplayerpos = player.transform.position + offset;
        if (InputManager.Instance.GetKey("PreBuildTower"))
        {
            followplayer = true;
        }

        if (followplayer)
        {
            transform.position = followplayerpos;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = Vector3.zero;

        }

        if (InputManager.Instance.GetKey("BuildTower"))
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}
