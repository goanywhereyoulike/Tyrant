using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTowerShoot : MonoBehaviour
{
    private GameObject currentTarget = null;
    Tower tower;
    PlayerMovement player;
    ChainTowerBullet Chainbullet;
    public Transform ShootPoint;
    private Vector2 ShootOffset;
    public GameObject BulletPrefab;
    public Transform TargetPos;
    public float DistanceToShoot;
    public float BulletForce = 20.0f;

    public float FireRate;
    float WaitFire = 0.0f;

    public Animator animator;
    private Vector3 direction;
    float angle;
    void Start()
    {
        tower = GetComponent<Tower>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        WaitFire += Time.deltaTime;
        if (currentTarget)
        {

            float Distance = (currentTarget.transform.position - transform.position).sqrMagnitude;
            if (Distance < DistanceToShoot * DistanceToShoot)
            {
                if (WaitFire > FireRate) //Fires gun everytime timer exceeds firerate
                {
                    WaitFire = 0.0f;
                    Fire();
                }
            }
        }
        

    }

    void Fire()
    {
        
        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("ChainTowerBullet");

        if (bullet)
        {
            bullet.transform.position = tower.transform.position;
            bullet.SetActive(true);
            Chainbullet = bullet.GetComponentInChildren<ChainTowerBullet>();
            //Chainbullet.GetComponent<DistanceJoint2D>().anchor = tower.transform.position;

            //GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position + Direction, ShootPoint.rotation);
            Rigidbody2D rb = Chainbullet.gameObject.GetComponent<Rigidbody2D>();
            Vector3 Direction = (transform.position-Chainbullet.transform.position).normalized;
            Vector3 NewDirection = Direction;
            NewDirection.x = 1.0f;
            NewDirection.y = -Direction.x / Direction.y;
            rb.velocity = NewDirection.normalized * BulletForce;

            
        }


    }

    void UpdateTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        GameObject target = GetClosestTarget(enemies);

        if (target != null)
        {
            float Distance = (target.transform.position - transform.position).sqrMagnitude;
            if (Distance < DistanceToShoot * DistanceToShoot)
            {
                currentTarget = target;
                TowerToTarget(target.transform);
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    GameObject GetClosestTarget(Enemy[] enemies)
    {
        GameObject retEnemy = null;
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (!enemies[i].IsDead)
            {
                if (retEnemy == null)
                {
                    retEnemy = enemies[i].gameObject;
                }
                else if ((enemies[i].transform.position - transform.position).sqrMagnitude < (retEnemy.transform.position - transform.position).sqrMagnitude)
                {
                    retEnemy = enemies[i].gameObject;
                }
            }
        }
        return retEnemy;
    }

    public void TowerToTarget(Transform targetpos)
    {

        direction = (targetpos.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Left
        if ((angle < -160.0f && angle >= -180.0f) || (angle > 160.0f && angle <= 180.0f))
        {
            animator.SetFloat("Direction", 0.05f);
        }
        //Left Up
        if (angle <= 160.0f && angle >= 100.0f)
        {
            animator.SetFloat("Direction", 0.2f);

        }
        //Up
        if (angle < 100.0f && angle > 80.0f)
        {
            animator.SetFloat("Direction", 0.3f);

        }
        //Right Up
        if (angle <= 80.0f && angle >= 10.0f)
        {
            animator.SetFloat("Direction", 0.5f);
        }
        //Right
        if (angle < 10.0f && angle > -10.0f)
        {
            animator.SetFloat("Direction", 0.6f);
        }
        //Right Down
        if (angle <= -10.0f && angle >= -80.0f)
        {
            animator.SetFloat("Direction", 0.72f);
        }
        //Down
        if (angle < -80.0f && angle > -100.0f)
        {
            animator.SetFloat("Direction", 0.9f);

        }
        //Left Down
        if (angle <= -100.0f && angle >= -160.0f)
        {
            animator.SetFloat("Direction", 1.0f);

        }


    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireSphere(transform.position, DistanceToShoot);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}
