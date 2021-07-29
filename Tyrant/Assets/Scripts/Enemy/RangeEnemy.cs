using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    // Start is called before the first frame update
    GameObject bObject;

     protected override void Start()
    {
        pathcount = 0;
        ObjectPoolManager.Instance.InstantiateObjects("enemyBullet");
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isGetBlock)
        {
            var tilemap = GameObjectsLocator.Instance.Get<Block>();
            nodePath.init(tilemap[0].tilemap.cellBounds.size.x, tilemap[0].tilemap.cellBounds.size.y);
            isGetBlock = true;
        }

        if (targets != null)
            targets.Clear();

        IsEnemyDead();
        detectObject();
        //behaviours.Update();

        // check mtarget is null or check deafult target is enemy,but not in range
        if (mTarget == null)
            findTarget = false;

        //check is base in the range, then attack base first
        if (mMainTarget != null)
        {
            mainTargetDistance = Vector3.Distance(transform.position, mMainTarget.position);
            if (IsTargetInRange(mainTargetDistance))
            {
                mTarget = mMainTarget;
                findTarget = true;
            }
        }

        //check enemy findtarget
        if (findTarget == false)
        {
            mTarget = mMainTarget;
            FindClosetObject();
            if(mTarget !=null)
                GetPath();

            if (findPath)
            {
                if (pathcount < mPath.Count)
                {
                    Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
                    float speed = MoveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, position, speed);
                    if ((Vector2)transform.position == position)
                    {
                        pathcount++;
                    }
                }
            }
        }
        else
        {
            if (mTarget != null)
                GetPath();

            distance = Vector3.Distance(transform.position, mTarget.position);
            if (IsTargetInRange(distance))
            {
                if (distance > stopDistance)
                {
                    //enemyState.force = behaviours.ForceCalculate();
                    //enemyState.acceleration = enemyState.force / enemyState.Mass;
                    //enemyState.velocity += enemyState.acceleration;
                    if (pathcount < mPath.Count)
                    {
                        Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
                        float speed = MoveSpeed * Time.deltaTime;
                        transform.position = Vector2.MoveTowards(transform.position, position, speed);
                        if ((Vector2)transform.position == position)
                        {
                            pathcount++;
                        }
                    }
                }
                else
                {
                    // enemyState.velocity = Vector3.zero;
                    if (Time.time >= attacktime)
                    {
                        if (mTarget != null)
                        {
                            Vector3 direction = mTarget.position - transform.position;
                            direction.Normalize();
                            bObject = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
                            bObject.transform.position = transform.position;
                            bObject.GetComponent<EnemyBullet>().Position = mTarget.position;
                            bObject.GetComponent<EnemyBullet>().Damage = (int)damage;
                            bObject.GetComponent<EnemyBullet>().Range = stopDistance;
                            bObject.GetComponent<EnemyBullet>().Direction = direction;
                            bObject.SetActive(true);
                            attacktime = Time.time + EnemyState.TimeBetweenAttacks;
                        }
                    }
                    //animation
                   // anim.SetBool("isRunning", false);
                }
            }
            else
            {
                findTarget = false;
            }
        }
        // transform.position += enemyState.velocity;
        for (int i = 0; i + 1 < mPath.Count; ++i)
        {
            var from = new Vector3(mPath[i].r, mPath[i].c);
            var to = new Vector3(mPath[i + 1].r, mPath[i + 1].c);
            Debug.DrawLine(from, to, Color.green);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, EnemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}
