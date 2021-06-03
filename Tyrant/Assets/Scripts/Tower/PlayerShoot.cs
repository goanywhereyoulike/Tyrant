using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject BulletPrefab;

    public float BulletForce = 20.0f;
    [SerializeField]
    float FireRate = 0.1f;
    float WaitFire = 0.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WaitFire += Time.deltaTime;
        if (InputManager.Instance.GetKeyDown("Shoot"))
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
        Vector3 TargetPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Direction = (TargetPos - transform.position).normalized;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + Direction, transform.rotation);
        if (bullet)
        {
            // bullet.transform.position = transform.position + Direction;
            //bullet.SetActive(true);
            //GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position + Direction, ShootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(Direction * BulletForce, ForceMode2D.Impulse);
        }


    }
}
