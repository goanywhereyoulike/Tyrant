using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// reference https://gamedev.stackexchange.com/questions/150917/how-to-get-all-tiles-from-a-tilemap
public class Block : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    public Tilemap tilemap;
    Vector3 worldPosition;
 
    public List<Vector2> blockObject;

    private void Awake()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            worldPosition.x = bounds.xMin + x;
            for (int y = 0; y < bounds.size.y; y++)
            {
                worldPosition.y = bounds.yMin + y;
                // x + y * bounds.size.x to know the tile position in array
                TileBase tile = tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    blockObject.Add(new Vector2(worldPosition.x, worldPosition.y));
                    Debug.Log("x:" + worldPosition.x + " y:" + worldPosition.y + " tile:" + tile.name);
                }
            }
            Debug.Log("boundx:" + bounds.xMin + " bounds:" + bounds.yMin);
        }
        RegisterToLocator();
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Block>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Block>(this);
    }
}
