using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnArea : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    public int spWidth;
    public int spHeight;
    public int spawnNumber;
    public int roomNumber;
    public string enemyType;
    private Vector2 spMax;
    private Vector2 spMin;
    private bool isTurnOff;
    public Vector2 SpMax { get => spMax; }
    public Vector2 SpMin { get => spMin; }
    public string EnemyType { get => enemyType; }
    public bool IsTurnOff { get => isTurnOff; set => isTurnOff = value; }

    private void Start()
    {
        Area();
        RegisterToLocator();
    }

    void Update()
    {
        if (IsTurnOff)
        {
            UnRegisterToLocator();
        }
    }
    void Area()
    {
        spMax.x = transform.position.x + spWidth;
        spMax.y = transform.position.y + spHeight;

        spMin.x = transform.position.x - spWidth;
        spMin.y = transform.position.y - spHeight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(SpMin, SpMax);
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<SpawnArea>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<SpawnArea>(this);
        gameObject.SetActive(false);
    }

    public void Turnoff(bool off)
    { 
        if(off)
        {
            IsTurnOff = true;
        }
        IsTurnOff = false;
    }
}

