using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class BossCharge : Action
{
    public BossFindTarget findTarget;
    private float moveSpeed;
    public float acceleration;
    private float startMoveTime;
    public float maxMoveSpeed;
    public float minMoveSpeed;


    private Rigidbody2D rb;
    private PSC psc;


    public override void OnStart()
    {
        psc = GetComponent<PSC>();
        startMoveTime = Time.time;
        AudioManager.Instance.Play("Charge");
    }

    public override TaskStatus OnUpdate()
    {
        if (psc.IsDead)
            return TaskStatus.Failure;
        Vector3 displacement = transform.position;
        float oldDistance = Vector3.Distance(transform.position, findTarget.TargetPos);
        if (oldDistance <= 0.01f)
        {
            psc.Animator.SetBool("Charge", false);
            return TaskStatus.Success;
        }

        moveSpeed = acceleration * (Time.time - startMoveTime);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
        Vector3 velocity = (findTarget.TargetPos - transform.position).normalized * moveSpeed * Time.deltaTime;
        displacement += velocity;
        float newDistance = Vector3.Distance(displacement, findTarget.TargetPos);
        transform.position = newDistance > oldDistance ? findTarget.TargetPos : displacement;
        if((transform.position-findTarget.TargetPos).normalized.x > 0.0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if ((transform.position - findTarget.TargetPos).normalized.x < 0.0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        psc.Animator.SetBool("Charge", true);
        //transform.position = findTarget.TargetPos;
        return TaskStatus.Running;
    }
}
