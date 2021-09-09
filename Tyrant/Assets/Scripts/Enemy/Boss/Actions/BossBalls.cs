using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class BossBalls : Action
{
    public BossFindTarget findTarget;
    public int maxAnmoCount = 3;
    public int range;
    public int damage;
    public int stayTime;

    private int anmoCount = 0;

    Vector2 max;
    Vector2 min;
    private PSC psc;

    public override void OnStart()
    {
        ObjectPoolManager.Instance.InstantiateObjects("BossBallBullet");
        psc = GetComponent<PSC>();
        anmoCount = maxAnmoCount;
        AttackArea();
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        if (!ObjectPoolManager.Instance.GetPooledObject("BossBallBullet"))
            return TaskStatus.Failure;

        Debug.DrawLine(transform.position, new Vector3(transform.position.x + range, transform.position.y, transform.position.z));

        if (anmoCount > 0)
        {
            switch (anmoCount)
            {
                case 1:
                    spawn(max);
                    break;
                case 2:
                    spawn(new Vector2(max.x, max.y - range));
                    break;
                case 3:
                    spawn(new Vector2(max.x, max.y - range * 2));
                    break;
                case 4:
                    spawn(new Vector2(min.x + range, min.y));
                    break;
                case 5:
                    spawn(new Vector2(min.x, min.y));
                    break;
                case 6:
                    spawn(new Vector2(min.x, min.y + range));
                    break;
                case 7:
                    spawn(new Vector2(min.x, min.y + range * 2));
                    break;
                case 8:
                    spawn(new Vector2(max.x - range, max.y));
                    break;
                default:
                    break;
            }
            anmoCount--;
            return TaskStatus.Running;
        }

        anmoCount = maxAnmoCount;

        return TaskStatus.Success;
    }

    private void AttackArea()
    {
        max = new Vector2(transform.position.x + range, transform.position.y + range);
        min = new Vector2(transform.position.x - range, transform.position.y - range);
    }
    private void spawn(Vector2 spPos)
    {
        var bullet = ObjectPoolManager.Instance.GetPooledObject("BossBallBullet");
        var bulletClass = bullet.GetComponent<BossBallsBullet>();
        bulletClass.Damage = damage;
        bulletClass.stayTime = stayTime;
        bulletClass.transform.position = spPos;
        bullet.SetActive(true);
    }
}