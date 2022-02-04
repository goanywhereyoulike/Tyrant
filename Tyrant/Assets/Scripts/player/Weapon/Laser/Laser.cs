using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Weapon
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator laserCompleteAnimator;

    private LaserStates laserStates = null;

    public LaserStates LaserStates { get => laserStates; set => laserStates = value; }

    private bool charged = false;
    private bool charging = false;

    public Slider chargingBar;
    public Slider durabilityBar;

    private float chargeTime;
    public float ChargeTime { get => chargeTime; set => chargeTime = value; }

    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }
 
        //chargingBar.gameObject.SetActive(false);
        LaserStates = weaponStates as LaserStates;

        ObjectPoolManager.Instance.InstantiateObjects("LaserBullet");
        ObjectPoolManager.Instance.InstantiateObjects("LaserBulletEffect");
        weaponInit = true;

        chargeTime = LaserStates.HoldingTime;
        chargingBar.maxValue = chargeTime;
        durability = laserStates.MaxDurability;
        durabilityBar.maxValue = laserStates.MaxDurability;
        durabilityBar.value = durability;
    }

    public override void ResetGun()
    {
        base.ResetGun();
        LaserStates = weaponStates as LaserStates;
        chargeTime = LaserStates.HoldingTime;
        chargingBar.maxValue = chargeTime;
        durability = laserStates.MaxDurability;
        durabilityBar.maxValue = laserStates.MaxDurability;
        durabilityBar.value = durability;
        animator.SetBool("Charging", false);
        laserCompleteAnimator.SetBool("charged", false);

        animator.enabled = true;
        laserCompleteAnimator.enabled = true;
        animator.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        laserCompleteAnimator.gameObject.GetComponent<SpriteRenderer>().sprite = null;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public override void UnFire()
    {
        base.UnFire();

        charging = false;
        AudioManager.Instance.Stop("Laser_Charging");
        animator.SetBool("Charging", charging);
        chargingBar.value = 0f;

        if (charged)
        {
            var bulletObject = ObjectPoolManager.Instance.GetPooledObject("LaserBullet");
            if (bulletObject)
            {
                var bullet = bulletObject.GetComponent<LaserBullet>();
                bullet.Damage = laserStates.Damage;
                bullet.ForzenTime = laserStates.FrozenTime;
                bullet.FrozenSpeed = laserStates.FrozenSpeed;
                bullet.BulletShootingSpeed = laserStates.BulletShootingSpeed;
                bullet.MovingRange = laserStates.ShootingRange;

                bullet.StartPosition = startShootingPointDict[Facing].transform.position;
                Vector2 different = InputManager.Instance.MouseWorldPosition - bullet.StartPosition;
                bullet.Direction = different.normalized;
                bulletObject.transform.position = bullet.StartPosition;
                Vector3 vecDir = InputManager.Instance.MouseWorldPosition - transform.position;
                bulletObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f - Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg);
                bulletObject.SetActive(true);
                AudioManager.Instance.Play("Shoot2");
            }
            charged = false;
            laserCompleteAnimator.SetBool("charged", charged);
            durability--;
            durabilityBar.value = durability;

            if (durability <= 0)
            {
                animator.enabled = false;
                laserCompleteAnimator.enabled = false;
            }
            CinemachineShaker.Instance.ShakeCamera(5f, 0.3f);
        }
    }

    public override void HoldingFire(float holdingTime)
    {
        base.HoldingFire(holdingTime);

        chargingBar.maxValue = chargeTime;
        chargingBar.gameObject.SetActive(true);

        if (!charging)
        {
            AudioManager.Instance.Play("Laser_Charging");
        }

        if (holdingTime >= chargeTime)
        {
            charged = true;
            laserCompleteAnimator.SetBool("charged", charged);
        }

        chargingBar.value = holdingTime;
        Vector3 vecDir = InputManager.Instance.MouseWorldPosition - transform.position;
        animator.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f - Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg);
        animator.gameObject.transform.position = startShootingPointDict[Facing].transform.position;
        laserCompleteAnimator.gameObject.transform.position = startShootingPointDict[Facing].transform.position;

        charging = true;
        animator.SetBool("Charging", charging);
    }
}
