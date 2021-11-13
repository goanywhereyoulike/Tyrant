using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingShoot : MonoBehaviour
{

    public int index = -1;

    public TowerTemplate towerData;
    [SerializeField]
    List<GameObject> Targets;
    [SerializeField]
    List<GameObject> Bullets;

    [SerializeField]
    Slider ColdDown;

    bool IsCoolDown = false;



    Tower tower;
    PlayerMovement player;
    public Transform ShootPoint;
    private Vector2 ShootOffset;
    public GameObject BulletPrefab;
    public Transform TargetPos;

    public float BulletLimit = 10;
    private float Bulletnum;
    public float CoolDownSpeed;


    public float DistanceToShoot;
    public float BulletForce = 20.0f;

    [SerializeField]
    float firenumber = 0.0f;


    [SerializeField]
    float attacknumber = 0.0f;

    void Start()
    {
        tower = GetComponentInParent<Tower>();
        IsCoolDown = false;
        Bulletnum = 0.0f;

        BulletLimit = towerData.BulletLimit;
        CoolDownSpeed = towerData.CoolDownSpeed;
        DistanceToShoot = towerData.distanceToShoot;
        firenumber = towerData.fireRate;
        BulletForce = towerData.bulletForce;

        ColdDown.maxValue = BulletLimit;
        ColdDown.value = float.IsNaN(Bulletnum) ? 0f : Bulletnum;
        ColdDown.fillRect.transform.GetComponent<Image>().color = Color.yellow;
        Targets = new List<GameObject>();
        Bullets = new List<GameObject>();
    }
    public void UpdateSlider()
    {

        ColdDown.value += 0f;


    }
    // Update is called once per frame

    void Update()
    {

        if (IsCoolDown)
        {
            List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
            if (enemies.Count == 0)
            {
                return;
            }
            foreach (var enemy in enemies)
            {

                LightingTowerBullet[] bullets = enemy.GetComponentsInChildren<LightingTowerBullet>();

                foreach (var bullet in bullets)
                {
                    if (bullet && bullet.TowerIndex == index)
                    {


                        //bullet.gameObject.transform.DetachChildren();
                        bullet.gameObject.transform.parent = null;
                        attacknumber--;
                        bullet.gameObject.SetActive(false);


                    }
                }
            }


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
        if (!IsCoolDown)
        {
            if (attacknumber > firenumber)
            {
                return;
            }
            AddTarget();
            Attack();
            ApplyDamage();

        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    string tag = collision.gameObject.tag;
    //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //    PSC levelBoss = collision.gameObject.GetComponent<PSC>();
    //    if (!IsCoolDown)
    //    {
    //        if (enemy || levelBoss)
    //        {
    //            if (!Targets.ContainsKey(collision.gameObject))
    //            {
    //                return;
    //            }
    //            Bulletnum += Time.deltaTime;
    //            ColdDown.value = Bulletnum;

    //            if (Bulletnum >= BulletLimit)
    //            {
    //                IsCoolDown = true;
    //            }

    //            if (enemy)
    //            {
    //                enemy.TakeDamage(towerData.bulletDamage * Time.deltaTime);
    //            }
    //            if (levelBoss)
    //            {
    //                levelBoss.TakeDamage(towerData.bulletDamage * Time.deltaTime);
    //            }
    //        }
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    string tag = collision.gameObject.tag;
    //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //    PSC levelBoss = collision.gameObject.GetComponent<PSC>();
    //    GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("LightingTowerBullet");
    //    if (bullet && !IsCoolDown)
    //    {
    //        if (enemy || levelBoss)
    //        {
    //            attacknumber++;
    //            if (attacknumber <= firenumber)
    //            {
    //                Targets.Add(collision.gameObject, true);
    //                Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
    //                bullet.GetComponent<LightingTowerBullet>().SetTarget(transform.position + offset, collision.gameObject.transform);
    //                if (enemy)
    //                {
    //                    bullet.gameObject.transform.parent = transform;

    //                }
    //                if (levelBoss)
    //                {
    //                    bullet.gameObject.transform.parent = transform;
    //                }
    //                bullet.SetActive(true);
    //            }



    //        }

    //    }

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    string tag = collision.gameObject.tag;
    //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //    PSC levelBoss = collision.gameObject.GetComponent<PSC>();

    //    if (enemy || levelBoss)
    //    {
    //        attacknumber--;
    //        Targets.Remove(collision.gameObject);
    //        LightingTowerBullet bullet = transform.parent.gameObject.GetComponentInChildren<LightingTowerBullet>();
    //        if (bullet)
    //        {
    //            bullet.gameObject.SetActive(false);


    //        }
    //    }



    //}

    void AddTarget()
    {
        bool aimtoBoss = false;

        if (GameObjectsLocator.Instance.Get<PSC>() != null)
        {
            List<PSC> level1boss = GameObjectsLocator.Instance.Get<PSC>();
            if (level1boss.Count != 0)
            {
                GameObject targetboss = level1boss[0].gameObject;
                float Distance = (targetboss.transform.position - transform.position).sqrMagnitude;
                if (Distance < DistanceToShoot * DistanceToShoot)
                {
                    aimtoBoss = true;
                    if (!Targets.Contains(targetboss))
                    {
                        Targets.Add(targetboss);
                    }

                }
                else
                {
                    if (Targets.Contains(targetboss))
                    {
                        Targets.Remove(targetboss);
                    }
                    aimtoBoss = false;
                }
            }
            else
            {
                aimtoBoss = false;
            }
        }
        if (!aimtoBoss)
        {
            if (GameObjectsLocator.Instance.Get<Enemy>() == null)
            {
                return;
            }

            List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
            foreach (var enemy in enemies)
            {
                float Distance = (enemy.transform.position - transform.position).sqrMagnitude;
                if (Distance < DistanceToShoot * DistanceToShoot)
                {
                    if (!Targets.Contains(enemy.gameObject) && (attacknumber < firenumber))
                    {

                        Targets.Add(enemy.gameObject);
                    }
                }
                else
                {
                    if (Targets.Contains(enemy.gameObject))
                    {
                        LightingTowerBullet bullet = enemy.GetComponentInChildren<LightingTowerBullet>();
                        if (bullet)
                        {
                            //bullet.gameObject.transform.DetachChildren();
                            bullet.gameObject.transform.parent = null;
                            attacknumber--;
                            bullet.gameObject.SetActive(false);
                            Bullets.Remove(bullet.gameObject);

                        }
                        Targets.Remove(enemy.gameObject);
                    }

                }
            }
        }
    }

    void ApplyDamage()
    {
        if (!IsCoolDown && attacknumber <= firenumber)
        {
            for (int i = 0; i < attacknumber; ++i)
            {
                if (Targets.Count > i)
                {
                    Enemy enemy = Targets[i].GetComponent<Enemy>();
                    enemy.TakeDamage(towerData.bulletDamage * Time.deltaTime);
                }


            }
        }


    }
    void Attack()
    {
        for (int i = 0; i < Targets.Count; ++i)
        {
            if (attacknumber > firenumber)
            {
                continue;
            }
            if (Targets.Count > i)
            {

                if (Targets[i] != null)
                {
                    Enemy enemy = Targets[i].GetComponent<Enemy>();


                    if (enemy)
                    {
                        Bulletnum += Time.deltaTime * 0.5f;
                        ColdDown.value = Bulletnum;

                        if (Bulletnum >= BulletLimit)
                        {
                            IsCoolDown = true;
                        }

                    }


                    if (enemy.IsDead)
                    {
                        LightingTowerBullet[] bullets = enemy.GetComponentsInChildren<LightingTowerBullet>();

                        foreach (var bullet in bullets)
                        {
                            if (bullet && bullet.TowerIndex == index)
                            {

                                //bullet.gameObject.transform.DetachChildren();
                                bullet.gameObject.transform.parent = null;
                                attacknumber--;
                                if (attacknumber <= 0)
                                {
                                    attacknumber = 0;
                                }
                                bullet.gameObject.SetActive(false);
                                Bullets.Remove(bullet.gameObject);

                            }
                        }

                        Targets.Remove(enemy.gameObject);
                    }
                    if (!IsCoolDown && Targets.Count > i)
                    {
                        Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
                        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("LightingTowerBullet");
                        bool AbleToAttack = true;
                        LightingTowerBullet[] lightingbullets = Targets[i].GetComponentsInChildren<LightingTowerBullet>();
                        if (lightingbullets.Length != 0)
                        {
                            foreach (var Lbullet in lightingbullets)
                            {
                                if (Lbullet.TowerIndex == index)
                                {
                                    AbleToAttack = false;
                                }

                            }


                        }

                        if (bullet && (attacknumber < firenumber) && AbleToAttack && Targets.Count > i)
                        {
                            bullet.transform.parent = Targets[i].transform;
                            bullet.GetComponent<LightingTowerBullet>().SetTarget(transform.position + offset, Targets[i].gameObject.transform);
                            bullet.SetActive(true);
                            bullet.GetComponent<LightingTowerBullet>().TowerIndex = index;
                            Bullets.Add(bullet);
                            attacknumber++;
                        }
                    }

                    //if (attacknumber <= firenumber)
                    //{
                    //    enemy.TakeDamage(towerData.bulletDamage * Time.deltaTime);
                    //    attacknumber++;
                    //}




                }
            }

        }

        //foreach (var target in Targets)
        //{
        //    Enemy enemy = target.GetComponent<Enemy>();
        //    // PSC levelBoss = target.GetComponent<PSC>();

        //    if (enemy)
        //    {
        //        if (!IsCoolDown)
        //        {
        //            Bulletnum += Time.deltaTime;
        //            ColdDown.value = Bulletnum;

        //            if (Bulletnum >= BulletLimit)
        //            {
        //                IsCoolDown = true;
        //            }


        //        }
        //    }
        //}
    }
    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(0.01f);
        bullet.SetActive(false);
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireSphere(transform.position, DistanceToShoot);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}
