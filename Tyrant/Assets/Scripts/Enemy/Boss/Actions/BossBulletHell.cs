using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossBulletHell : Action
{
    public int bulletCount = 0;
    public int bulletMoveSpeed = 0;
    public float bulletDelayTime = 0;
    public bool inverseAngle = false;

    private float timeCheck = 0;
    private float angle = 0f;
    private int count = 0;
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

        if (timeCheck < bulletDelayTime)
        {
            timeCheck += Time.deltaTime;
            return TaskStatus.Running;
        }

        timeCheck = 0;

        if (count > 0)
        {
            var bullet = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
            var bulletClass = bullet.GetComponent<EnemyBullet>();

            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            bulletClass.Position = bulDir * 100.0f;
            bulletClass.transform.position = transform.position;
            bulletClass.bulletSpeed = bulletMoveSpeed;
            bullet.SetActive(true);
            count--;
            angle = inverseAngle? angle - 10f : angle + 10f;

            return TaskStatus.Running;
        }

        count = bulletCount;

        return TaskStatus.Success;
    }
}
