using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Transform targetpos;
    private Vector2 direction;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.position - targetpos.position).normalized;
        if (direction.x >0.8f)
        {
            animator.SetFloat("Direction", 0.1f);
        }
        if (direction.y < -0.8f)
        {
            animator.SetFloat("Direction", 0.333f);
        }
        if (direction.x < -0.8f)
        {
            animator.SetFloat("Direction", 0.66f);
        }
        if (direction.y > 0.8f)
        {
            animator.SetFloat("Direction", 1.0f);
        }


    }
}
