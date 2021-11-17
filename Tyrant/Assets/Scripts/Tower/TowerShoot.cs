using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShoot : MonoBehaviour
{

    public enum TowerFace {Right,RightUp,RightDown,Left,LeftUp,LeftDown,Up,Down };
    public TowerTemplate towerData;

    [SerializeField]
    Material DissolveMaterial;

    [SerializeField]
    Slider ColdDown;

    bool IsCoolDown = false;

    [SerializeField]
    bool HasAnimation = true;

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
    public TowerFace towerface;

    public Animator animator;
    private Vector3 direction;
    float angle;
    void Start()
    {
        tower = GetComponent<Tower>();
        IsChainTower = false;
        IsCoolDown = false;
        Bulletnum = 0.0f;

        BulletLimit = towerData.BulletLimit;
        CoolDownSpeed = towerData.CoolDownSpeed;
        DistanceToShoot = towerData.distanceToShoot;
        FireRate = towerData.fireRate;
        BulletForce = towerData.bulletForce;

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
        if (currentTarget && currentTarget.gameObject.activeSelf)
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
        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("TowerBullet");
        //if (towerInfo)
        //{
        if (towerData.type == "Basic" && bullet)
        {
            AudioManager.instance.PlaySFX(14);
            bullet.GetComponent<TowerBullet>().bulletDamage = towerData.bulletDamage;
        }
        if (towerData.type == "CannonTower")
        {
            bullet = ObjectPoolManager.Instance.GetPooledObject("CannonTowerBullet");
            if (bullet)
            {
                AudioManager.instance.PlaySFX(15);
                bullet.GetComponent<CannonTowerBullet>().bulletDamage = towerData.bulletDamage;
              
            }

        }

        if (towerData.type == "ChainTower")
        {
            bullet = ObjectPoolManager.Instance.GetPooledObject("ChainTowerBullet");
            if (bullet)
            {
                bullet.transform.parent = transform;
                bullet.GetComponentInChildren<ChainTowerBullet>().bulletDamage = towerData.bulletDamage;
                IsChainTower = true;
                bullet.GetComponent<DistanceJoint2D>().connectedAnchor = transform.position;
            }

        }
        //if (towerData.type == "LightingTower")
        //{
        //    bullet = ObjectPoolManager.Instance.GetPooledObject("LightingTowerBullet");
        //    if (bullet)
        //    {
        //        bullet.GetComponent<LightingTowerBullet>().bulletDamage = towerData.bulletDamage;
        //        Vector3 offset=new Vector3(0.0f,0.5f,0.0f);

        //        bullet.GetComponent<LightingTowerBullet>().SetTarget(transform.position+offset, currentTarget.transform);
        //    }



        //}
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
                if (rb)
                {
                    rb.AddForce(Direction * BulletForce, ForceMode2D.Impulse);
                }


                StartCoroutine(DestroyBullet(bullet));

            }

        }


    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1.0f);
        bullet.SetActive(false);
    }

    void UpdateTarget()
    {
        // GameObject[] enemyobjs = GameObject.FindGameObjectsWithTag("Enemy");
        bool aimtoBoss = false;

        if (GameObjectsLocator.Instance.Get<PSC>() != null)
        {
            List<PSC> level1boss = GameObjectsLocator.Instance.Get<PSC>();
            GameObject targetboss = GetClosestTarget(level1boss);
            if (targetboss != null)
            {
                float Distance = (targetboss.transform.position - transform.position).sqrMagnitude;
                if (Distance < DistanceToShoot * DistanceToShoot)
                {
                    aimtoBoss = true;
                    currentTarget = targetboss;
                    TowerToTarget(targetboss.transform);
                }
                else
                {
                    aimtoBoss = false;
                }
            }
            else
            {
                aimtoBoss = false;
                currentTarget = null;
            }
        }
        if (!aimtoBoss)
        {
            if (GameObjectsLocator.Instance.Get<Enemy>() == null)
            {
                return;
            }

            List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
            GameObject target = GetClosestTarget(enemies);
            if (target != null)
            {
                float Distance = (target.transform.position - transform.position).sqrMagnitude;
                if (Distance < DistanceToShoot * DistanceToShoot)
                {
                    currentTarget = target;
                    if (HasAnimation)
                    {
                        TowerToTarget(target.transform);
                    }

                }
            }
            else
            {
                currentTarget = null;
            }
        }
    }
    GameObject GetClosestTarget(List<PSC> bosses)
    {
        GameObject retBoss = null;
        for (int i = 0; i < bosses.Count; ++i)
        {
            if (!bosses[i].IsDead)
            {
                if (retBoss == null)
                {
                    retBoss = bosses[i].gameObject;
                }
                else if ((bosses[i].transform.position - transform.position).sqrMagnitude < (retBoss.transform.position - transform.position).sqrMagnitude)
                {
                    retBoss = bosses[i].gameObject;
                }
            }
        }
        return retBoss;
    }


    GameObject GetClosestTarget(List<Enemy> enemies)
    {
        GameObject retEnemy = null;
        for (int i = 0; i < enemies.Count; ++i)
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
            towerface = TowerFace.Left;
        }
        //Left Up
        if (angle <= 160.0f && angle >= 100.0f)
        {
            animator.SetFloat("Direction", 0.2f);
            towerface = TowerFace.LeftUp;
        }
        //Up
        if (angle < 100.0f && angle > 80.0f)
        {
            animator.SetFloat("Direction", 0.3f);
            towerface = TowerFace.Up;

        }
        //Right Up
        if (angle <= 80.0f && angle >= 10.0f)
        {
            animator.SetFloat("Direction", 0.5f);
            towerface = TowerFace.RightUp;
        }
        //Right
        if (angle < 10.0f && angle > -10.0f)
        {
            animator.SetFloat("Direction", 0.6f);
            towerface = TowerFace.Right;
        }
        //Right Down
        if (angle <= -10.0f && angle >= -80.0f)
        {
            animator.SetFloat("Direction", 0.72f);
            towerface = TowerFace.RightDown;
        }
        //Down
        if (angle < -80.0f && angle > -100.0f)
        {
            animator.SetFloat("Direction", 0.9f);
            towerface = TowerFace.Down;

        }
        //Left Down
        if (angle <= -100.0f && angle >= -160.0f)
        {
            animator.SetFloat("Direction", 1.0f);
            towerface = TowerFace.LeftDown;

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
