using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponStates weaponStates;

    private float nextFireTime = 0.0f;

    [SerializeField]
    private List<WeaponFacing.WeaponFacingObject> startShootingPoints = null;
    protected Dictionary<PlayerFacing, GameObject> startShootingPointDict = null;

    public PlayerFacing Facing { get; set; }

    protected bool canFire = false;

    protected virtual void Start()
    {
        startShootingPointDict = new Dictionary<PlayerFacing, GameObject>();

        foreach (var item in startShootingPoints)
        {
            startShootingPointDict.Add(item.playerFacing, item.gameObject);
        }
    }

    public virtual void Fire()
    {
        canFire = false;
        if (weaponStates.IsLocked)
        {
            Debug.LogWarning($"Current weapon {weaponStates.name} is locked.");
            return;
        }
        if (weaponStates.MaxBulletPerClipCount <= 0)
        {
            Debug.LogWarning($"Current weapon {weaponStates.name} has to reload bullets.");
            return;
        }
        if (weaponStates.MaxBulletCount <= 0)
        {
            Debug.LogWarning($"Current weapon {weaponStates.name} has no bullets.");
            return;
        }

        if (Time.time < nextFireTime)
            return;
        else
        {
            nextFireTime = Time.time + weaponStates.ShootingDelayTime;
            canFire = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, weaponStates.ShootingRange);
    }
}
