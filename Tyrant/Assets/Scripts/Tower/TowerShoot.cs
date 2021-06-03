using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    PlayerMovement player;
    public Transform ShootPoint;
    private Vector2 ShootOffset;
    public GameObject BulletPrefab;
    public Transform TargetPos;
    public float DistanceToShoot;
    public float BulletForce = 20.0f;
    [SerializeField]
    float FireRate = 0.1f;
    float WaitFire = 0.0f;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        TargetPos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        WaitFire += Time.deltaTime;

        float Distance = (TargetPos.position - transform.position).sqrMagnitude;
        if (Distance < DistanceToShoot * DistanceToShoot)
        {
            if (WaitFire > FireRate) //Fires gun everytime timer exceeds firerate
            {
                WaitFire = 0.0f;
                Fire();
            }
        }

    }

    void Fire()
    {
        Vector3 Direction = (TargetPos.position - ShootPoint.position).normalized;
        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("TowerBullet");
        if (bullet)
        {
            bullet.transform.position = ShootPoint.position + Direction;
            bullet.SetActive(true);
            //GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position + Direction, ShootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(Direction * BulletForce, ForceMode2D.Impulse);
        }


    }
}
