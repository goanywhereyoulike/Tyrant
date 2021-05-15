using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : AiBehaviours
{
    // Update is called once per frame
    override public Vector3 behaviour(Enemy enemy) 
    {
        Vector3 ToTarget = enemy.target.position - enemy.transform.position;
        ToTarget.Normalize();
        Vector3 desiredVelocity = ToTarget * enemy.EnemyState.EnemyMoveSpeed * Time.deltaTime;
        Vector3 velocity = enemy.EnemyState.velocity;
        //enemy.position = Vector3.MoveTowards(enemy.position, target.position, enemySpeed * Time.deltaTime);
        return desiredVelocity - velocity;
    }
}
