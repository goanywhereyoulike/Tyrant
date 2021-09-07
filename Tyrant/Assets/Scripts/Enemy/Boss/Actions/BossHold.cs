using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BossHold : Action
{
    public float holdTime;
    public Slider holdSlider;
    private float timeCheck;
    private PSC psc;

    public override void OnStart()
    {
        psc = GetComponent<PSC>();
        holdSlider.maxValue = holdTime;
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDied)
            return TaskStatus.Failure;

        if (timeCheck <= holdTime)
        {
            timeCheck += Time.deltaTime;
            holdSlider.value = timeCheck;
            return TaskStatus.Running;
        }
        timeCheck = 0;
        holdSlider.value = timeCheck;
        return TaskStatus.Success;

    }
}

