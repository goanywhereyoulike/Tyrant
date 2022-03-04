using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// reference https://gamedev.stackexchange.com/questions/150917/how-to-get-all-tiles-from-a-tilemap
public class Block : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    public List<Tilemap> tilemaps;
    
    Vector2 wPosition;
    int sizeX;
    int sizeY;
    public List<Vector2> worldPosition;
    //public List<Vector2> gridPosition;
    private List<Vector2> blockObject = new List<Vector2>();

    public int SizeX { get => sizeX; }
    public int SizeY { get => sizeY; }
    public List<Vector2> BlockObject { get => blockObject;}

    private void Start()
    {
        for (int i = 0; i< tilemaps.Count;i++)
        {
            GetTiles(tilemaps[i], i);
        }
        GetTileSize();
       
        RegisterToLocator();
    }
    //USE FOR DEBUG TILE
    //private void Update()
    //{
    //    drawBlock();
    //}
    void GetTileSize()
    {
        //if (i + 1 < tilemaps.Count)
        //{
        //    if (tilemaps[i].size.x > tilemaps[i + 1].size.x)
        //    {
        //        sizeX = tilemaps[i].size.x;
        //    }
        //    else
        //    {
        //        sizeX = tilemaps[i + 1].size.x;
        //    }
        //    if (tilemaps[i].size.y > tilemaps[i + 1].size.y)
        //    {
        //        sizeY = tilemaps[i].size.y;
        //    }
        //    else
        //    {
        //        sizeY = tilemaps[i + 1].size.y;
        //    }
        //}
        sizeX = tilemaps[1].size.x;
        sizeY = tilemaps[1].size.y;
    }

    void GetTiles(Tilemap tileMap, int i)
    {
        BoundsInt bounds = tileMap.cellBounds;
        TileBase[] tiles = tileMap.GetTilesBlock(bounds);
        List<Vector2> block = new List<Vector2>();
        for (int y = 0; y < bounds.size.y; y++)
        {
                wPosition.y = bounds.yMin + y;
            for (int x = 0; x < bounds.size.x; x++)
            {
                wPosition.x = bounds.xMin + x;
                if (i == 1)
                {
                    worldPosition.Add(wPosition);
                }
                // x + y * bounds.size.x to know the tile position in array
                TileBase tile = tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    blockObject.Add(new Vector2(wPosition.x, wPosition.y));
                }
            }
        }
    }

    //void drawBlock()
    //{
    //    for(int i =0;i<blockObject.Count;i++)
    //    {
    //        Debug.DrawRay(blockObject[i], Vector2.up, Color.red, 0.5f);
    //        Debug.DrawRay(blockObject[i], Vector2.left, Color.red, 0.5f);
    //        Debug.DrawRay(blockObject[i], Vector2.right, Color.red, 0.5f);
    //        Debug.DrawRay(blockObject[i], Vector2.down, Color.red, 0.5f);
    //    }
    //}
    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Block>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Block>(this);
    }
}
