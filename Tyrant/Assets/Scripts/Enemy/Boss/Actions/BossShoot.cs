using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossShoot : Action
{
    public BossFindTarget findTarget;
    public int maxAmmoCount = 3;

    private int ammoCount = 0;
    public float speed;
    public float orignalSpeed;

    private PSC psc;
    public override void OnStart()
    {
        
        psc = GetComponent<PSC>();
        ammoCount = maxAmmoCount;
        orignalSpeed = speed;
        psc.shootAnimFinished += onShootAnimatnionFinished;
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDead)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("enemyBullet"))
            return TaskStatus.Failure;

        if (ammoCount > 0)
        {
            if (!psc.Animator.GetBool("Spell"))
            {
                Debug.Log("Spell");
                
                psc.Animator.SetBool("Spell", true);
                AudioManager.Instance.Play("FireBall");
            }

            //StartCoroutine(WaitFor(0.5f));
            //yield return new WaitForSeconds(f);
            if (psc.Animator.IsInTransition(0))
            {
               
            }

          

        }
        else
        {
            //ammoCount = maxAmmoCount;
            AudioManager.Instance.Stop("FireBall");
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    public void onShootAnimatnionFinished()
    {
        //for (int i = 0; i < maxAmmoCount; ++i)
        while(ammoCount>0)
        {
            var bullet = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
            var bulletClass = bullet.GetComponent<EnemyBullet>();
            bulletClass.Position = findTarget.TargetPos;
            bulletClass.transform.position = psc.FirePoint.position;
            bulletClass.bulletSpeed = speed;
            //bulletClass.Damage = 10;
            speed += 5;
            speed = speed > 30 ? speed-10 : speed;
            ammoCount--;
            bullet.SetActive(true);
        }
        speed = orignalSpeed;
        psc.Animator.SetBool("Spell", false);
       


    }

    /*IEnumerator WaitFor(float f)
    {
       


    }*/
}
