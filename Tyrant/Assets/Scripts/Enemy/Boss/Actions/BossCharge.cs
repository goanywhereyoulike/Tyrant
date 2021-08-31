using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossCharge : Action
{
    public BossFindTarget findTarget;

    private Rigidbody2D rb;
    private PSC psc;

    public override void OnStart()
    {
        psc = GetComponent<PSC>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        transform.position = findTarget.TargetPos;

        return TaskStatus.Success;
    }
}
