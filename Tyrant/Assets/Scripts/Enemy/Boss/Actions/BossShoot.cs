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
        if (psc.IsDead)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("enemyBullet"))
            return TaskStatus.Failure;

        if (anmoCount > 0)
        {
            psc.Animator.SetBool("Spell", true);
            
            StartCoroutine(WaitFor(0.5f));
            
            return TaskStatus.Running;



        }

        anmoCount = maxAnmoCount;

        return TaskStatus.Success;
    }

    IEnumerator WaitFor(float f)
    {
        yield return new WaitForSeconds(f);
        var bullet = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
        var bulletClass = bullet.GetComponent<EnemyBullet>();
        bulletClass.Position = findTarget.TargetPos;
        bulletClass.transform.position = transform.position;
        bulletClass.bulletSpeed = speed;
        speed += 5;
        speed = speed > 30 ? speed - 10 : speed;
        anmoCount--;
        bullet.SetActive(true);
        psc.Animator.SetBool("Spell", false);
    }
}
