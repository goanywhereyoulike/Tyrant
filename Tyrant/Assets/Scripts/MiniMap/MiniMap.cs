using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    GameObject MiniMapUI;

    bool IsOpen = true;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (InputManager.Instance.GetKeyDown("MiniMap"))
        {
            MiniMapUI.SetActive(IsOpen);
            IsOpen = !IsOpen;
        }
    }
    private void LateUpdate()
    {

        Vector3 newposition = player.position;
        newposition.z = transform.position.z;
        transform.position = newposition;
    }
}
