using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// reference https://gamedev.stackexchange.com/questions/150917/how-to-get-all-tiles-from-a-tilemap
public class Block : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    public Tilemap tilemap;
    Vector2 wPosition;

    public List<Vector2> worldPosition;
    public List<Vector2> gridPosition;
    public List<Vector2> blockObject;

    private void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        for (int y = 0; y < bounds.size.y; y++)
        {
            wPosition.y = bounds.yMin + y;
            for (int x = 0; x < bounds.size.x; x++)
            {
                wPosition.x = bounds.xMin + x;
                worldPosition.Add(wPosition);
                //worldPosition.Add(new Vector2(wPosition.x + 0.5f, wPosition.y));
                //worldPosition.Add(new Vector2(wPosition.x, wPosition.y + 0.5f));
                //worldPosition.Add(new Vector2(wPosition.x + 0.5f, wPosition.y + 0.5f));

                // Debug.Log("boundx:" + wPosition.x + " bounds:" + wPosition.y);
                gridPosition.Add(new Vector2(x,y));
                // x + y * bounds.size.x to know the tile position in array
                TileBase tile = tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    //blockObject.Add(new Vector2(wPosition.x, wPosition.y));
                    blockObject.Add(new Vector2(wPosition.x, wPosition.y));
                    Debug.Log("x:" + wPosition.x + " y:" + wPosition.y + " tile:" + tile.name);
                }
            }
           // Debug.Log("boundx:" + bounds.size.x + " bounds:" + bounds.size.y);
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
