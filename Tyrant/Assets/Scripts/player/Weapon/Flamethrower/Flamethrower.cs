using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flamethrower : Weapon
{
    private FlamethrowerStates flamethrowerStates = null;
    [SerializeField]
    private ParticleSystem flameParticle = null;
    //public float maxAmmo;
    public Slider ammoBar;
    private float currentAmmo;
    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }
        flamethrowerStates = weaponStates as FlamethrowerStates;

        ammoBar.gameObject.SetActive(false);

        ammoBar.maxValue = flamethrowerStates.MaxAmmo;
        currentAmmo = flamethrowerStates.MaxAmmo;
        ammoBar.value = flamethrowerStates.MaxAmmo;

        
        flameParticle.Stop();
        flameParticle.gameObject.GetComponent<FlamethrowerBullet>().BurnDamage = flamethrowerStates.BurnDamage;
        weaponInit = true;
    }

    public override void Fire()
    {
        base.Fire();
        if (canFire && currentAmmo > 0)
        {
            ammoBar.gameObject.SetActive(true);
            Vector3 vecDir = InputManager.Instance.MouseWorldPosition - flameParticle.gameObject.transform.position;
            ParticleSystem.MainModule mainMod = flameParticle.main;
            mainMod.startRotation = Mathf.Atan2(-vecDir.y, vecDir.x);
            ParticleSystem.ShapeModule shapeModule = flameParticle.shape;
            shapeModule.rotation = new Vector3(Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg, 90.0f, 0.0f);
            flameParticle.gameObject.transform.position = startShootingPointDict[Facing].transform.position;
            flameParticle.Play();
            ammoBar.value = currentAmmo;
            AudioManager.instance.PlaySFX(2);
        }
        currentAmmo--;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public override void UnFire()
    {
        base.UnFire();
        //ammoBar.value = currentAmmo;
        flameParticle.Stop();
    }
}
