using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingShoot : MonoBehaviour
{

    public TowerTemplate towerData;

    List<LightingTowerBullet> bullets;

    [SerializeField]
    Slider ColdDown;

    bool IsCoolDown = false;

    [SerializeField]
    bool HasAnimation = true;

    bool IsAttacking = false;

    private GameObject currentTarget = null;
    private List<GameObject> AttackableTargets;
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
    float firetime = 0.0f;
    float duration = 0.0f;

    private Vector3 direction;
    float angle;
    void Start()
    {
        tower = GetComponentInParent<Tower>();
        IsCoolDown = false;
        Bulletnum = 0.0f;

        BulletLimit = towerData.BulletLimit;
        CoolDownSpeed = towerData.CoolDownSpeed;
        DistanceToShoot = towerData.distanceToShoot;
        firetime = towerData.fireRate;
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
        if (IsCoolDown)
        {
            LightingTowerBullet bullet = transform.parent.gameObject.GetComponentInChildren<LightingTowerBullet>();
            if (bullet)
            {
                bullet.gameObject.SetActive(false);

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
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();
        if (!IsCoolDown)
        {
            if (enemy || levelBoss)
            {
                Bulletnum += Time.deltaTime;
                ColdDown.value = Bulletnum;

                if (Bulletnum >= BulletLimit)
                {
                    IsCoolDown = true;
                }

                if (enemy)
                {
                    enemy.TakeDamage(towerData.bulletDamage * Time.deltaTime);
                }
                if (levelBoss)
                {
                    levelBoss.TakeDamage(towerData.bulletDamage * Time.deltaTime);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();
        GameObject bullet = ObjectPoolManager.Instance.GetPooledObject("LightingTowerBullet");

        if (bullet && !IsCoolDown)
        {
            if (enemy || levelBoss)
            {
                bullet.GetComponent<LightingTowerBullet>().bulletDamage = towerData.bulletDamage;
                Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
                bullet.GetComponent<LightingTowerBullet>().SetTarget(transform.position + offset, collision.gameObject.transform);
                if (enemy)
                {
                    bullet.gameObject.transform.parent = transform;

                }
                if (levelBoss)
                {
                    bullet.gameObject.transform.parent = transform;
                }
                bullet.SetActive(true);

            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();

        if (enemy || levelBoss)
        {
            LightingTowerBullet bullet = transform.parent.gameObject.GetComponentInChildren<LightingTowerBullet>();
            if (bullet)
            {
                bullet.gameObject.SetActive(false);

            }
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
