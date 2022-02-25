using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossBulletHell : Action
{
    public int bulletCount = 0;
    public int bulletMoveSpeed = 0;
    public float bulletDelayTime = 0;
    public bool inverseAngle = false;
    public float bulletDelayTimeChange = 0;

    private float timeCheck = 0;
    private float angle = 0f;
    private int count = 0;
    private PSC psc;
    private Vector3 direction = Vector3.up;
    

    public override void OnStart()
    {
        ObjectPoolManager.Instance.InstantiateObjects("BossBullet");
        psc = GetComponent<PSC>();
        psc.Animator.SetBool("SpellLoop", true);
        AudioManager.Instance.Play("DarkMagic");

    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDead)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("BossBullet"))
            return TaskStatus.Failure;

        if (timeCheck < bulletDelayTime)
        {
            timeCheck += Time.deltaTime;
            return TaskStatus.Running;
        }

        timeCheck = 0;

        if (count > 0)
        {
            var bullet = ObjectPoolManager.Instance.GetPooledObject("BossBullet");
            var bulletClass = bullet.GetComponent<BossBullet>();

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * direction.x - Mathf.Sin(angle * Mathf.Deg2Rad) * direction.y;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * direction.x + Mathf.Cos(angle * Mathf.Deg2Rad) * direction.y;
            //float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            //float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            // Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            //Vector3 bulMoveVector = Quaternion.AngleAxis(angle, Vector3.up) * transform.position;
            //Vector3 bulDir = (bulMoveVector - transform.position).normalized;
            Vector3 bulDir = new Vector3(x, y, 0).normalized;
            float bulAngle = Mathf.Atan2(bulDir.y, bulDir.x) * Mathf.Rad2Deg;
            bulletClass.gameObject.transform.rotation = Quaternion.AngleAxis(bulAngle, Vector3.forward);
            bulletClass.Position = psc.FirePoint.position + bulDir * 100.0f;
            bulletClass.transform.position = psc.FirePoint.position;
            bulletClass.bulletSpeed = bulletMoveSpeed;
            //bulletClass.Direction = bulDir;
            bullet.SetActive(true);
            count--;
            direction = bulDir;
           // angle = inverseAngle? angle - 10f : angle + 10f;
            angle = inverseAngle?  - 10f :  + 10f;

           return TaskStatus.Running;
        }

        count = bulletCount;
        psc.Animator.SetBool("SpellLoop", false);

        AudioManager.Instance.Stop("DarkMagic");

        return TaskStatus.Success;
    }
}
