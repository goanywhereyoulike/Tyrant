using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapChange : MonoBehaviour
{

    [SerializeField]
    Tilemap curtilemap;

    [SerializeField]
    TileBase currentTile;

    [SerializeField]
    Camera camera;

    [SerializeField]
    Vector3Int offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = curtilemap.WorldToCell(camera.ScreenToWorldPoint(transform.position)) + offset;
        if (InputManager.Instance.GetKeyDown("Interact"))
        {

            PlaceTile(pos);
        
        }
    }
    void PlaceTile(Vector3Int pos)
    {
        curtilemap.SetTile(pos, currentTile);
    
    }

}
