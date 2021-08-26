using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get => _instance; }

    //  List<Enemy> enemies = new List<Enemy>();

    public int noSpawn;
    public Tilemap tilemap;

    Enemy enemy;

    // public Transform[] spawnPoints;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

    }

    //private void Update()
    //{
    //    var enemies = GameObjectsLocator.Instance.Get<Enemy>();
    //    if (enemies != null)
    //    {
    //        for (int i = 0; i < enemies.Count;)
    //        {
    //            if (enemies[i].mTarget != null && !enemies[i].FindPath)
    //            {
    //                enemies[i].GetPath();
    //                if (enemies[i].FindPath)
    //                {
    //                    i++;
    //                }
    //            }
    //            else
    //            {
    //                if (enemies[i].Pathcount > 3)
    //                {
    //                    enemies[i].GetPath();
    //                    if (enemies[i].FindPath)
    //                    {
    //                        i++;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}