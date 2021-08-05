using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShoot : MonoBehaviour
{
    [SerializeField]
    Slider ColdDown;

    bool IsCoolDown = false;
   
    private GameObject currentTarget = null;
    Tower tower;
    PlayerMovement player;
    public Transform ShootPoint;
    private Vector2 ShootOffset;
    public GameObject BulletPrefab;
    public Transform TargetPos;

    public float BulletLimit = 10;
    private float Bulletnum;
    public float CoolDownSpeed;


    private bool IsChainTower;

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
        IsChainTower = false;
        IsCoolDown = false;
        Bulletnum = 0.0f;
        ColdDown.maxValue = BulletLimit;
        ColdDown.value = float.IsNaN(Bulletnum) ? 0f : Bulletnum;
        ColdDown.fillRect.transform.GetComponent<Image>().color = Color.yellow;
    }
    public void UpdateSlider()
    {

        ColdDown.value += 0f;


    }
    // Update is called once per frame
    void Update()
    {
        ColdDown.maxValue = BulletLimit;
        UpdateTarget();
        WaitFire += Time.deltaTime;
        if (IsCoolDown)
        {
            ColdDown.fillRect.transform.GetComponent<Image>().color = Color.red;

            Bulletnum -= CoolDownSpeed * Time.deltaTime;
            ColdDown.value = Bulletnum;

            if (Bulletnum <= 0)
            {
                Bulletnum = 0;
                ColdDown.value = Bulletnum;
                ColdDown.fillRect.transform.GetComponent<Image>().color = Color.yellow;
                IsCoolDown = false;
            }


        }
        if (currentTarget)
        {

            float Distance = (currentTarget.transform.position - transform.position).sqrMagnitude;
            if (Distance < DistanceToShoot * DistanceToShoot)
            {
                if (WaitFire > FireRate && !IsCoolDown) //Fires gun everytime timer exceeds firerate
                {
                    WaitFire = 0.0f;
                    Bulletnum++;
                    ColdDown.value = Bulletnum;
                    if (Bulletnum >= BulletLimit)
                    {
                        IsCoolDown = true;
                    }
                    Fire();

                }
            }
        }


    }

    void BulletColdDown()
    {
        Bulletnum--;
        ColdDown.value = Bulletnum;
    }



    void Fire()
    {
        Vector3 Direction = (currentTarget.transform.position - ShootPoint.position).normalized;
        TowerDisplay towerInfo = gameObject.GetComponent<TowerDisplay>();
        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("TowerBullet");
        if (towerInfo)
        {
            if (towerInfo.tower.type == "Basic")
            {
                bullet.GetComponent<TowerBullet>().bulletDamage = towerInfo.tower.bulletDamage;
            }
            if (towerInfo.tower.type == "CannonTower")
            {
                bullet = ObjectPoolManager.Instance.GetPooledObject("CannonTowerBullet");
                bullet.GetComponent<CannonTowerBullet>().bulletDamage = towerInfo.tower.bulletDamage;
            }

            if (towerInfo.tower.type == "ChainTower")
            {
                bullet = ObjectPoolManager.Instance.GetPooledObject("ChainTowerBullet");
                bullet.GetComponentInChildren<ChainTowerBullet>().bulletDamage = towerInfo.tower.bulletDamage;
                IsChainTower = true;
            }
        }

        if (bullet)
        {
            if (IsChainTower)
            {
                bullet.transform.position = tower.transform.position;
                bullet.SetActive(true);
                ChainTowerBullet Chainbullet = bullet.GetComponentInChildren<ChainTowerBullet>();
                Rigidbody2D crb = Chainbullet.gameObject.GetComponent<Rigidbody2D>();
                Vector3 cDirection = (transform.position - Chainbullet.transform.position).normalized;
                Vector3 NewDirection = cDirection;
                NewDirection.x = 1.0f;
                NewDirection.y = -cDirection.x / cDirection.y;
                crb.velocity = NewDirection.normalized * BulletForce;

            }
            else
            {
                bullet.transform.position = ShootPoint.position + Direction;
                bullet.SetActive(true);
                //GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position + Direction, ShootPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                rb.AddForce(Direction * BulletForce, ForceMode2D.Impulse);

            }

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
