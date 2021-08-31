using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossShoot : Action
{
    public BossFindTarget findTarget;

    private PSC psc;

    public override void OnStart()
    {
        ObjectPoolManager.Instance.InstantiateObjects("enemyBullet");
        psc = GetComponent<PSC>();
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("enemyBullet"))
            return TaskStatus.Failure;

        var bullet = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
        var bulletClass = bullet.GetComponent<EnemyBullet>();
        bulletClass.Position = findTarget.TargetPos;
        bulletClass.bulletSpeed = 20;
        bullet.SetActive(true);

        return TaskStatus.Success;
    }
}
