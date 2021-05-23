using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();

    //public Transform target;

    private List<Transform> targets = new List<Transform>();
    public Transform mTarget;
    private float distance = 0f;
    private float lastDistance = 0f;
    bool findTarget = false;

    StaticMachine behaviours = new StaticMachine();
   
    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

   


    /// <summary>

    private Animator anim;
/// </summary>


// Start is called before the first frame update
    void Awake()
    {
        behaviours = gameObject.GetComponent<StaticMachine>();
        behaviours.setEnemy(this);
        behaviours.AllBehaviour();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        detectObject();
        behaviours.Update();

        if(findTarget==false)
        {
            FindClosetObject();
        }
        else
        {
            if (Vector3.Distance(transform.position, mTarget.position) > enemyState.StopDistance)
            {
                enemyState.force = behaviours.ForceCalculate();
                enemyState.acceleration = enemyState.force / enemyState.Mass;
                enemyState.velocity += enemyState.acceleration;

                //animation
                anim.SetBool("isRunning", true);
            }
            else
            {
                enemyState.velocity = Vector3.zero;
                /*
                            if (Time.time >= enemyState.TimeBetweenAttacks)
                            {

                                enemyState.TimeBetweenAttacks = Time.time + enemyState.TimeBetweenAttacks;
                            }
                */
                //animation
                anim.SetBool("isRunning", false);
            }
            transform.position += enemyState.velocity;
        }
       
       
       

    }
    /*------------------攻击位移------------------------
    IEnumerator Attack()
    {
        //player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;

        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }
    */
    void Animation()
    {
        anim.SetBool("isRunning", true);
    }

    void detectObject()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null || GameObject.FindGameObjectsWithTag("Tower") != null)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Player"))
            {
                targets.Add(target.GetComponent<Transform>());
            }

            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Tower"))
            {
                targets.Add(target.GetComponent<Transform>());
            }
        }
    }
    
    void FindClosetObject()
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            distance = Vector3.Distance(transform.position, targets[i].position);
            if (lastDistance == 0)
            {
                lastDistance = distance;
            }
            else if (distance < lastDistance)
            {
                lastDistance = distance;
                mTarget = targets[i];
                findTarget = true;
            }
            else
            {
                mTarget = targets[0];
                findTarget = true;
            }
        }

    }
}