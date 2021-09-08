using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CheckHealth : Action
{
    public float minTargetHealth = 0f;
    public float maxTargetHealth = 0f;

    public PSC TargetBoss = null;

    public override TaskStatus OnUpdate()
    {
        if (TargetBoss.Health < maxTargetHealth && TargetBoss.Health > minTargetHealth)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}