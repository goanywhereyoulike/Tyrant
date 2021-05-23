using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seeking : AiBehaviours
{
    [SerializeField]
    private bool isOn;
    public bool IsOn { get => isOn; set => isOn = value; }

    override public void update() 
    {
        IsActive = IsOn;
    }
    override public Vector3 behaviour(Enemy enemy) 
    {
        Vector3 ToTarget = enemy.mTarget.position - enemy.transform.position;
        ToTarget.Normalize();
        Vector3 desiredVelocity = ToTarget * enemy.EnemyState.EnemyMoveSpeed * Time.deltaTime;
        Vector3 velocity = enemy.EnemyState.velocity;
        //enemy.position = Vector3.MoveTowards(enemy.position, target.position, enemySpeed * Time.deltaTime);
        return desiredVelocity - velocity;
    }
}
