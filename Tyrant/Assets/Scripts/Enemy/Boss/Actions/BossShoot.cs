using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossShoot : Action
{
    public BossFindTarget findTarget;
    public int maxAnmoCount = 3;

    private int anmoCount = 0;
    public float speed;

    private PSC psc;

    public override void OnStart()
    {
        ObjectPoolManager.Instance.InstantiateObjects("enemyBullet");
        psc = GetComponent<PSC>();
        anmoCount = maxAnmoCount;
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("enemyBullet"))
            return TaskStatus.Failure;

        if (anmoCount > 0)
        {
            var bullet = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
            var bulletClass = bullet.GetComponent<EnemyBullet>();
            bulletClass.Position = findTarget.TargetPos;
            bulletClass.transform.position = transform.position;
            bulletClass.bulletSpeed = speed;
            speed += 5;
            speed = speed > 30 ? speed - 10 : speed;
            bullet.SetActive(true);
            anmoCount--;
            return TaskStatus.Running;
        }

        anmoCount = maxAnmoCount;

        return TaskStatus.Success;
    }
}
