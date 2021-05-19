using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public int health;

    [HideInInspector]
    public Transform player;

    public float speed;

    public float timeBetweenAttacks;

    public int damage;

    public int pickupChance;
    public string areaToLoad;
    public GameObject[] pickups;
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }




}
