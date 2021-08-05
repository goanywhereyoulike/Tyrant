using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    private FlamethrowerStates flamethrower = null;
    [SerializeField]
    private ParticleSystem flameParticle = null;
    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }

        flamethrower = weaponStates as FlamethrowerStates;
        flameParticle.Stop();
        weaponInit = true;
    }

    public override void Fire()
    {
        base.Fire();

        if (canFire)
        {
            Vector3 vecDir = InputManager.Instance.MouseWorldPosition - flameParticle.gameObject.transform.position;
            ParticleSystem.MainModule mainMod = flameParticle.main;
            mainMod.startRotation = Mathf.Atan2(-vecDir.y, vecDir.x);
            ParticleSystem.ShapeModule shapeModule = flameParticle.shape;
            shapeModule.rotation = new Vector3(Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg, 90.0f, 0.0f);
            flameParticle.gameObject.transform.position = startShootingPointDict[Facing].transform.position;
            flameParticle.Play();
        }
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public override void UnFire()
    {
        base.UnFire();

        flameParticle.Stop();
    }
}
