using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;

public class ChainTowerShoot : MonoBehaviour
{

    public enum TowerFace { Right, RightUp, RightDown, Left, LeftUp, LeftDown, Up, Down };
    public TowerTemplate towerData;

    [SerializeField]
    Material DissolveMaterial;

    [SerializeField]
    Slider ColdDown;

    bool IsCoolDown = false;

    [SerializeField]
    bool HasAnimation = true;

    //private GameObject currentTarget = null;
    Tower tower;
    PlayerMovement player;
    public Transform ShootPoint;
    private Vector2 ShootOffset;
    public GameObject BulletPrefab;
    public Transform TargetPos;

    [SerializeField]
    private int totalBulletNum = 0;
    private float BulletLimit = 10;
    private float Bulletnum;
    private float CoolDownSpeed;
    private float DistanceToShoot;
    private float BulletForce = 20.0f;
    private float FireRate;
    float WaitFire = 0.0f;
    public TowerFace towerface;

    private Vector3 direction;
    float angle;
    void Start()
    {
        tower = GetComponent<Tower>();
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


        if (WaitFire > FireRate && !IsCoolDown && totalBulletNum < 8) //Fires gun everytime timer exceeds firerate
        {
            WaitFire = 0.0f;
            Bulletnum++;

            ColdDown.value = Bulletnum;
            if (Bulletnum >= BulletLimit)
            {
                IsCoolDown = true;
                return;
            }
            Fire();
        }



    }

    public void TotalBulletDecrease()
    {
        totalBulletNum--;
        if (totalBulletNum <= 0)
        {
            totalBulletNum = 0;
        }
    }
    void BulletColdDown()
    {
        Bulletnum--;
        ColdDown.value = Bulletnum;
    }



    void Fire()
    {

        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("ChainTowerBullet");
        if (bullet)
        {
            totalBulletNum++;
            bullet.transform.parent = transform;
            bullet.GetComponentInChildren<ChainTowerBullet>().bulletDamage = towerData.bulletDamage;
            bullet.GetComponent<DistanceJoint2D>().connectedAnchor = transform.position;
        }

        if (bullet)
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


    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1.0f);
        bullet.SetActive(false);
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
            if (!enemies[i].IsDead && enemies[i].gameObject.activeInHierarchy)
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

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireSphere(transform.position, DistanceToShoot);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}
