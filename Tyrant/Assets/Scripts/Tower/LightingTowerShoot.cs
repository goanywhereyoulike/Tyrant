using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;

public class LightingTowerShoot : MonoBehaviour
{

    public int index = -1;
    public TowerTemplate towerData;
    [SerializeField]
    Slider ColdDown;

    [SerializeField]
    List<GameObject> Targets;
    [SerializeField]
    List<GameObject> AttackedTargets;

    bool IsCoolDown = false;
    Tower tower;

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
    // Start is called before the first frame update
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
        AttackedTargets= new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsCoolDown)
        {
            CoolDownTime();
        }
        else
        {
            if (attacknumber > firenumber)
            {
                return;
            }
            Attack();
            Targets.RemoveAll(target => !target.activeInHierarchy);
            AttackedTargets.RemoveAll(target => !target.activeInHierarchy);
        }



    }
    void Attack()
    {
        bool aimtoBoss = false;
        if (aimtoBoss)
        {
            AddBossTargets(ref aimtoBoss);
        }
        else
        {
            AddEnemyToTargets(ref aimtoBoss);
        }
        AttachLightingBullet();
        CheckBullet();
        ApplyDamage();

    }
    void ApplyDamage()
    {
        foreach (GameObject target in AttackedTargets)
        {
            if (!target.activeInHierarchy)
            {
                continue;
            }
            Enemy enemy = target.GetComponent<Enemy>();
            PSC boss = target.GetComponent<PSC>();
            if (enemy)
            {
                enemy.TakeDamage(towerData.bulletDamage * Time.deltaTime);
            }
            if (boss)
            {
                boss.TakeDamage(towerData.bulletDamage * Time.deltaTime);

            }
        }

    }
    void CheckBullet()
    {
        List<PSC> levelbosses = GameObjectsLocator.Instance.Get<PSC>();
        List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
        if (enemies == null && levelbosses == null)
        {
            return;
        }
        if (levelbosses != null)
        {
            foreach (var boss in levelbosses)
            {
                LightingTowerBullet[] bullets = boss.GetComponentsInChildren<LightingTowerBullet>();
                foreach (var bullet in bullets)
                {
                    if (bullet && bullet.TowerIndex == index)
                    {
                        if (!AttackedTargets.Contains(boss.gameObject))
                        {
                            bullet.gameObject.transform.parent = null;
                            attacknumber--;
                            bullet.gameObject.SetActive(false);
                        }

                    }
                }
            }
        }
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                LightingTowerBullet[] bullets = enemy.GetComponentsInChildren<LightingTowerBullet>();
                foreach (var bullet in bullets)
                {
                    if (bullet && bullet.TowerIndex == index)
                    {
                        if (!AttackedTargets.Contains(enemy.gameObject))
                        {
                            bullet.gameObject.transform.parent = null;
                            attacknumber--;
                            bullet.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }


    }
    void AttachLightingBullet()
    {
        if (Targets == null || Targets.Count == 0)
        {
            return;
        }
        foreach (GameObject target in Targets)
        {
            if (attacknumber > firenumber)
            {
                return;
            }
            if (target != null && target.activeInHierarchy)
            {
                Bulletnum += Time.deltaTime * 0.5f;
                ColdDown.value = Bulletnum;
                if (Bulletnum >= BulletLimit)
                {
                    IsCoolDown = true;
                }
                Enemy enemy = target.GetComponent<Enemy>();
                PSC levelBoss = target.GetComponent<PSC>();

                Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
                GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("LightingTowerBullet");
                bool AbleToAttack = true;
                LightingTowerBullet[] lightingbullets = target.GetComponentsInChildren<LightingTowerBullet>();
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
                if (bullet && (attacknumber < firenumber) && AbleToAttack)
                {
                    float Distance = (target.transform.position - transform.position).sqrMagnitude;
                    if (Distance > DistanceToShoot * DistanceToShoot)
                    {
                        continue;
                    }
                    bullet.GetComponent<LightingTowerBullet>().TowerIndex = index;
                    bullet.transform.parent = target.transform;
                    bullet.GetComponent<LightingTowerBullet>().SetTarget(transform.position + offset, target.gameObject.transform);
                    bullet.SetActive(true);
                    AttackedTargets.Add(target);
                    attacknumber++;
                }


            }
        }
    }

    void RemoveBulletFromTarget(GameObject target)
    {
        AttackedTargets.Remove(target);
        LightingTowerBullet[] bullets = target.GetComponentsInChildren<LightingTowerBullet>();
        foreach (var bullet in bullets)
        {
            if (bullet && bullet.TowerIndex == index)
            {
                bullet.gameObject.transform.parent = null;
                attacknumber--;
                if (attacknumber <= 0)
                {
                    attacknumber = 0;
                }
                bullet.gameObject.SetActive(false);
            }
        }
    }
    void AddEnemyToTargets(ref bool aimtoBoss)
    {
        if (GameObjectsLocator.Instance.Get<Enemy>() == null)
        {
            return;
        }

        List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
        foreach (var enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy || enemy == null)
            {
                continue;
            }
            if (enemy.IsDead)
            {
                if (Targets.Contains(enemy.gameObject))
                {
                    RemoveBulletFromTarget(enemy.gameObject);
                    Targets.Remove(enemy.gameObject);
                }
            }
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
                    RemoveBulletFromTarget(enemy.gameObject);
                    Targets.Remove(enemy.gameObject);
                }
            }
        }
    }


    void AddBossTargets(ref bool aimtoBoss)
    {
        if (GameObjectsLocator.Instance.Get<PSC>() != null)
        {
            List<PSC> level1boss = GameObjectsLocator.Instance.Get<PSC>();
            if (level1boss != null)
            {
                GameObject targetboss = level1boss[0].gameObject;
                if (level1boss[0].IsDead)
                {
                    if (Targets.Contains(targetboss))
                    {
                        RemoveBulletFromTarget(targetboss);
                        Targets.Remove(targetboss);
                    }
                    aimtoBoss = false;
                }
                var bossSpawn = targetboss.GetComponent<BehaviorTree>().FindTask<BossSpawn>();
                if (!bossSpawn.IsSpawn)
                {
                    if (Targets.Contains(targetboss))
                    {
                        RemoveBulletFromTarget(targetboss);
                        Targets.Remove(targetboss);
                    }
                    aimtoBoss = false;
                }
                else
                {
                    float Distance = (targetboss.transform.position - transform.position).sqrMagnitude;
                    if (Distance < DistanceToShoot * DistanceToShoot)
                    {
                        aimtoBoss = true;
                        if (!Targets.Contains(targetboss) && targetboss.activeInHierarchy && (attacknumber < firenumber))
                        {
                            Targets.Add(targetboss);
                        }
                    }
                    else
                    {
                        if (Targets.Contains(targetboss))
                        {
                            RemoveBulletFromTarget(targetboss);
                            Targets.Remove(targetboss);
                        }
                        aimtoBoss = false;
                    }

                }
            }
            else
            {
                aimtoBoss = false;
            }
        }
    }
    void CoolDownTime()
    {
        Targets.Clear();
        AttackedTargets.Clear();
        List<PSC> levelbosses = GameObjectsLocator.Instance.Get<PSC>();
        List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
        if (enemies == null && levelbosses == null)
        {
            return;
        }
        if (levelbosses != null)
        {
            foreach (var boss in levelbosses)
            {
                LightingTowerBullet[] bullets = boss.GetComponentsInChildren<LightingTowerBullet>();
                foreach (var bullet in bullets)
                {
                    if (bullet && bullet.TowerIndex == index)
                    {
                        bullet.gameObject.transform.parent = null;
                        attacknumber--;
                        bullet.gameObject.SetActive(false);
                    }
                }
            }
        }
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                LightingTowerBullet[] bullets = enemy.GetComponentsInChildren<LightingTowerBullet>();
                foreach (var bullet in bullets)
                {
                    if (bullet && bullet.TowerIndex == index)
                    {
                        bullet.gameObject.transform.parent = null;
                        attacknumber--;
                        bullet.gameObject.SetActive(false);
                    }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireSphere(transform.position, DistanceToShoot);
    }
}
