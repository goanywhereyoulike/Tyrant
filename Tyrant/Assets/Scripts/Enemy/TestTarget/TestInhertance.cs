using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInhertance : MonoBehaviour
{
    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    pathcount = 0;
    //    base.Start();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!isGetBlock)
    //    {
    //        var tilemap = GameObjectsLocator.Instance.Get<Block>();
    //        nodePath.init(tilemap[0].tilemap.cellBounds.size.x, tilemap[0].tilemap.cellBounds.size.y);
    //        isGetBlock = true;
    //    }

        

    //    if (targets != null)
    //        targets.Clear();

    //    IsEnemyDead();
    //    detectObject();
    //    //behaviours.Update();

    //    // check mtarget is null or check deafult target is enemy,but not in range
    //    if (mTarget == null)
    //        findTarget = false;

    //    //check is base in the range, then attack base first
    //    mainTargetDistance = Vector3.Distance(transform.position, mMainTarget.position);
    //    if (IsTargetInRange(mainTargetDistance))
    //    {
    //        mTarget = mMainTarget;
    //        findTarget = true;
    //    }


    //    //check enemy findtarget
    //    if (findTarget == false)
    //    {
    //        mTarget = mMainTarget;
    //        FindClosetObject();
    //        if (search == false)
    //        {
    //            GetPath();
    //        }

    //        if (findPath)
    //        {
    //            if (pathcount < mPath.Count)
    //            {
    //                Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
    //                float speed = MoveSpeed * Time.deltaTime;
    //                transform.position = Vector2.MoveTowards(transform.position, position, speed);
    //                if ((Vector2)transform.position == position)
    //                {
    //                    pathcount++;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (search == true)
    //        {
    //            GetPath();
    //            pathcount = 0;
    //        }
    //        distance = Vector3.Distance(transform.position, mTarget.position);
    //        if (IsTargetInRange(distance))
    //        {
    //            if (distance > stopDistance)
    //            {
    //                //enemyState.force = behaviours.ForceCalculate();
    //                //enemyState.acceleration = enemyState.force / enemyState.Mass;
    //                //enemyState.velocity += enemyState.acceleration;
    //                if (pathcount < mPath.Count)
    //                {
    //                    Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
    //                    float speed = MoveSpeed * Time.deltaTime;
    //                    transform.position = Vector2.MoveTowards(transform.position, position, speed);
    //                    if ((Vector2)transform.position == position)
    //                    {
    //                        pathcount++;
    //                    }
    //                }
    //                //animation
    //                anim.SetBool("isRunning", true);

    //            }
    //            else
    //            {
    //                // enemyState.velocity = Vector3.zero;
    //                if (Time.time >= attacktime)
    //                {
    //                    StartCoroutine("Attack");
    //                    attacktime = Time.time + EnemyState.TimeBetweenAttacks;
    //                }
    //                //animation
    //                anim.SetBool("isRunning", false);
    //            }
    //            search = false;
    //        }
    //        else
    //        {
    //            findTarget = false;
    //        }
    //    }
    //    // transform.position += enemyState.velocity;
    //    for (int i = 0; i + 1 < mPath.Count; ++i)
    //    {
    //        var from = new Vector3(mPath[i].r, mPath[i].c);
    //        var to = new Vector3(mPath[i + 1].r, mPath[i + 1].c);
    //        Debug.DrawLine(from, to, Color.green);
    //    }
    //}
}
