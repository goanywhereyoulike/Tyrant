using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void LateUpdate()
    {

        Vector3 newposition = player.position;
        newposition.z = transform.position.z;
        transform.position = newposition;
    }
}
