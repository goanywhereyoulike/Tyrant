using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : AiBehaviours
{
    override public Vector3 behaviour(Enemy enemy)
    {
        Vector3 ToTarget = enemy.target.position - enemy.transform.position;
        double dist = Vector3.Distance(enemy.transform.position, enemy.target.position);

        if (dist > 0)
        {
            const double DecelWeater = 0.3;
            float speed = (float)dist / (float)DecelWeater;
            speed = Mathf.Min(speed, enemy.EnemyState.EnemyMoveSpeed);
            Vector3 desiredVelocity = ToTarget * enemy.EnemyState.EnemyMoveSpeed * Time.deltaTime;
            return desiredVelocity - enemy.EnemyState.velocity;
        }
        return Vector3.zero;
    }
}
