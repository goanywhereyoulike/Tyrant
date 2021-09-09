using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BossFindTarget : Action
{
    private Vector3 targetPos = Vector3.zero;
    private PSC psc;

    public Vector3 TargetPos { get => targetPos; private set => targetPos = value; }
    public override void OnStart()
    {
        psc = GetComponent<PSC>();
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        if (!GameObjectsLocator.Instance.Get<Player>()[0])
            return TaskStatus.Failure;

        TargetPos = GameObjectsLocator.Instance.Get<Player>()[0].transform.position;
        return TaskStatus.Success;
    }
}
